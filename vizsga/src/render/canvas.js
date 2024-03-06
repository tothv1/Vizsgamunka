import React, { useRef, useEffect } from 'react'
import { rawMaps } from '../Assets/map/maps';

import { Slime } from '../system/Slime';
import { Wall } from '../system/StoneWall';
import { Player } from '../system/Player';
import { DMGpopup } from './DmgPopup';
import '../system/Math';
import { Clamp, Normalise, CheckCollision, getRandomRange } from '../system/Math';



let renderOffset = [0, 0]
let gameSize = [0, 0]
let aimOffset = [0, 0];


const Canvas = props => {

  const canvasRef = useRef(null)

  gameSize = [props.style.width, props.style.height]


  let entities = [];

  entities.projectileList = [];
  entities.tileList = [];
  entities.entityList = [];
  entities.effectList = [];
  entities.hpBarList = [];

  let mapsize = [rawMaps[0][0].length * 64, rawMaps[0].length * 64];




  const playerRef = Object.create(Player);
  playerRef.x = 600;
  playerRef.y = 600;
  playerRef.mapsize = mapsize;
  playerRef.entityRef = entities;

  playerRef.canvasRef = canvasRef.current;

  entities.entityList.push(playerRef);

  let frameCount = 0

  const clrCanvas = (ctx) => {
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)

  }

  const draw = (ctx, object, offset) => {
    ctx.save();
    if (object.rotation != undefined || object.rotation != 0) {
      ctx.translate(offset[0] + object.width / 2, offset[1] + object.height / 2);

      ctx.rotate(object.rotation);
      ctx.translate(-offset[0] - object.width / 2, -offset[1] - object.height / 2);
      ctx.drawImage(object.drawing, object.frame * object.width, 0, object.width, object.height, offset[0], offset[1], object.width, object.height);
      ctx.rotate(-object.rotation);

    } else {
      ctx.drawImage(object.drawing, object.frame * object.width, 0, object.width, object.height, offset[0], offset[1], object.width, object.height);

    }
    ctx.restore();
  }

  const drawText = (ctx, object, offset) => {

    ctx.globalAlpha = object.opacity;
    ctx.font = `${object.size}px Joystix Monospace`;
    ctx.fillText(`${object.damage}`, offset[0], offset[1]);
    ctx.globalAlpha = 1.0;

  }

  const drawHPBar = (ctx, object, offset) => {
    ctx.fillStyle = 'black';
    ctx.beginPath();
    ctx.rect(offset[0], offset[1], object.width, object.height);
    ctx.closePath();
    ctx.fill();

    ctx.fillStyle = 'red';
    ctx.beginPath();
    ctx.rect(offset[0], offset[1], object.width * object.ratio, object.height);
    ctx.closePath();
    ctx.fill();

    ctx.fillStyle = 'black';
  }

  const rawmap = rawMaps[0];
  for (let i = 0; i < rawmap.length; i++) {
    for (let j = 0; j < rawmap[i].length; j++) {
      if (rawmap[i][j] === 1) {
        const temp = Object.create(Wall);
        temp.x = j * 64;
        temp.y = i * 64;
        entities.tileList.push(temp);
      }
      if (rawmap[i][j] === 2) {
        const temp = Object.create(Slime);
        temp.x = j * 64;
        temp.y = i * 64;
        entities.entityList.push(temp);
      }
    }
  }
  console.log(entities)
  console.log(playerRef)

  useEffect(() => {


    document.addEventListener("keydown", (event) => {
      playerRef.keyhandler(event)
    });
    document.addEventListener("keyup", (event) => {
      playerRef.keyhandler(event)
    });
    document.addEventListener("click", (event) => {
      entities.projectileList.push(playerRef.shoot(event, playerRef, aimOffset))
    });

    const canvas = canvasRef.current
    const context = canvas.getContext('2d')

    var rect = canvas.getBoundingClientRect();
    aimOffset = [rect.left,rect.top]
    console.log(rect.top, rect.right, rect.bottom, rect.left);

    //console.log(entities)

    let animationFrameId = 0

    let Runtime = 0;
    let lastUpdateTime = 0;

    //Our draw came here
    const render = () => {
      clrCanvas(context);
      Runtime = window.performance.now();
      let deltaTime = (Runtime - lastUpdateTime) / 1000


      //entity update
      entities.entityList.forEach(item => {
        item.update(deltaTime, frameCount, playerRef);
        item.renderoffset = renderOffset;
        if (item.ID === 1) {
          item.offset = [item.xcenter + renderOffset[0], item.ycenter + renderOffset[1]];
          draw(context, item, item.offset);
          item.hpbar.setval(item.maxHealth, item.health);
          drawHPBar(context, item.hpbar, item.offset);

        }
        if (item.dead) {
          entities.entityList = entities.entityList.filter((xd) => !xd.dead);
        }

      });

      // tile update
      entities.tileList.forEach(item => {
        item.update(deltaTime, frameCount);
        item.offset = [item.x + renderOffset[0], item.y + renderOffset[1]];
        draw(context, item, item.offset)
      });


      //projectile update
      entities.projectileList.forEach(item => {
        item.update(deltaTime, frameCount);

        entities.entityList.forEach(element => {
          if (CheckCollision(item, element) && item.hitlimit > 0) {



            item.hitlimit--;
            element.takeDamage(item.damage);
            let temp = Object.create(DMGpopup);
            temp.x = element.x;
            temp.y = element.y;
            temp.damage = item.damage;
            temp.size = Math.sqrt(item.damage) + 20;
            temp.drift = [getRandomRange(-100, 100), -500];

            entities.effectList.push(temp);
            if (item.hitlimit <= 0) {
              item.dead = true;
            }

          }
        });

        if (item.x < -500 || item.x > mapsize[0] + 500 || item.y < -500 || item.y > mapsize[1] + 500) {
          item.dead = true;
        }
        entities.projectileList = entities.projectileList.filter((xd) => !xd.dead);

        item.offset = [item.xcenter + renderOffset[0], item.ycenter + renderOffset[1]];
        draw(context, item, item.offset)
      });

      //effect update
      entities.effectList.forEach(item => {
        item.update(deltaTime, frameCount);
        if (item.frame > item.maxFrame) {
          entities.effectList.splice(item, 1);
        }
        item.offset = [item.x + renderOffset[0], item.y + renderOffset[1]];
        drawText(context, item, item.offset);
      });



      renderOffset = [Clamp(playerRef.x - gameSize[0] / 2, 0, (rawmap[0].length * 64) - gameSize[0]), Clamp(playerRef.y - gameSize[1] / 2, 0, (rawmap.length * 64) - gameSize[1])]
      renderOffset = [-renderOffset[0], -renderOffset[1]]

      let playerrenderpos = [playerRef.x + renderOffset[0], playerRef.y + renderOffset[1]]
      draw(context, playerRef, [playerrenderpos[0] - Player.width / 2, playerrenderpos[1] - Player.height / 2]);

      lastUpdateTime = window.performance.now();
      frameCount++
      animationFrameId = window.requestAnimationFrame(render)
    }
    render()

    return () => {
      window.cancelAnimationFrame(animationFrameId)
    }
  }, [draw])

  return (
    <div className={"d-flex align-items-center justify-content-center vh-100"}>
      <canvas width={gameSize[0]} height={gameSize[1]} className='mg-0 b-0' ref={canvasRef} {...props} />
    </div>
  )
}

export default Canvas