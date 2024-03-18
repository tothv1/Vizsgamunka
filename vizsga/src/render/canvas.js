import React, { useRef, useEffect } from 'react'
import { rawMaps } from '../Assets/map/maps';

import { Slime, xd } from '../system/Slime';
import { Wall } from '../system/StoneWall';
import { Player } from '../system/Player';
import { DMGpopup } from './DmgPopup';
import '../system/Math';
import { Clamp, Normalise, CheckCollision, getRandomRange } from '../system/Math';
import { Bow } from '../system/Weapons/Bow';

import { XP } from '../system/Pickups/Experience';
import { Spawner } from '../system/Spawner';
import { Potion } from '../system/Pickups/Potion';

let renderOffset = [0, 0]
let gameSize = [0, 0]
let windowSize = [0, 0];

let aimpoint = [0, 0];
let entities = [];

let gameTime = 0;
let paused = false;

const Canvas = props => {

  const canvasRef = useRef(null)
  gameSize = [props.style.width, props.style.height]
  let frameCount = 0

  const clrCanvas = (ctx) => {
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)
  }

  const draw = (ctx, object, offset) => {
    ctx.save();

    ctx.translate(offset[0] + object.width / 2, offset[1] + object.height / 2);
    ctx.rotate(object.rotation);
    ctx.translate(-offset[0] - object.width / 2, -offset[1] - object.height / 2);
    ctx.drawImage(object.drawing, object.frame * object.width, 0, object.width, object.height, offset[0], offset[1], object.width, object.height);
    ctx.rotate(-object.rotation);

    ctx.restore();
  }

  const drawText = (ctx, object, offset) => {

    ctx.globalAlpha = object.opacity;
    ctx.font = `${object.size}px Joystix Monospace`;

    if (object.Damage < 0) {
      ctx.fillStyle = '#00ff66';
      ctx.fillText(`${object.Damage.toString().substring(1)}`, offset[0], offset[1]);

      ctx.strokeStyle = 'black';
      ctx.strokeText(`${object.Damage.toString().substring(1)}`, offset[0], offset[1]);
      ctx.globalAlpha = 1.0;
    } else {

      if (object.critLevel == 0) {
        ctx.fillStyle = 'white';
        ctx.fillText(`${object.Damage}`, offset[0], offset[1]);
      }
      if (object.critLevel == 1) {
        ctx.fillStyle = 'yellow';
        ctx.fillText(`${object.Damage}`, offset[0], offset[1]);
      }
      if (object.critLevel == 2) {
        ctx.fillStyle = 'orange';
        ctx.fillText(`${object.Damage}`, offset[0], offset[1]);
      }
      if (object.critLevel >= 3) {
        ctx.fillStyle = 'red';
        ctx.fillText(`${object.Damage}`, offset[0], offset[1]);
      }

      ctx.strokeStyle = 'black';
      ctx.strokeText(`${object.Damage}`, offset[0], offset[1]);
      ctx.globalAlpha = 1.0;
    }
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

  const drawXPBar = (ctx, object) => {


    ctx.font = `20px Joystix Monospace`;

    ctx.fillStyle = 'yellow';
    ctx.fillText(`Lv.${object.level}`, 10, 30);

    ctx.strokeStyle = 'black';
    ctx.strokeText(`Lv.${object.level}`, 10, 30);
    ctx.globalAlpha = 1.0;

    ctx.font = `15px Joystix Monospace`;

    ctx.fillStyle = 'yellow';
    ctx.fillText(`XP:${object.requiredXP}/${object.currrentXP}`, 10, 50);

    ctx.strokeStyle = 'black';
    ctx.strokeText(`XP:${object.requiredXP}/${object.currrentXP}`, 10, 50);
    ctx.globalAlpha = 1.0;

    ctx.fillStyle = 'black';
    ctx.beginPath();
    ctx.rect(100, 20, (props.style.width - 120), 10);
    ctx.closePath();
    ctx.fill();

    ctx.fillStyle = 'yellow';
    ctx.beginPath();
    ctx.rect(100, 20, (props.style.width - 120) * object.XPRatio, 10);
    ctx.closePath();
    ctx.fill();

    ctx.fillStyle = 'black';

    ctx.font = `15px Joystix Monospace`;

    var sec = Math.floor(gameTime)
    var min = Math.floor(sec / 60);
    sec -= min * 60;



    ctx.fillStyle = 'white';
    ctx.fillText(`${min}:${sec}`, 10, 70);

    ctx.strokeStyle = 'black';
    ctx.strokeText(`${min}:${sec}`, 10, 70);
    ctx.globalAlpha = 1.0;

  }

  const drawPausedUI = (ctx, object, offset) => {
    ctx.save();


    ctx.globalAlpha = 0.5;

    ctx.fillStyle = 'black';
    ctx.beginPath();
    ctx.rect(0, 0, ctx.canvas.width, ctx.canvas.height);
    ctx.closePath();
    ctx.fill();

    ctx.globalAlpha = 1.0;


    ctx.restore();
  }


  useEffect(() => {

    const canvas = canvasRef.current
    const context = canvas.getContext('2d')
    var rect = canvas.getBoundingClientRect();
    windowSize = [rect.left, rect.top]

    // init

    entities.projectileList = [];
    entities.tileList = [];
    entities.entityList = [];
    entities.effectList = [];
    entities.hpBarList = [];

    let mapsize = [rawMaps[0][0].length * 64, rawMaps[0].length * 64];

    const playerRef = new Player();
    playerRef.x = 600;
    playerRef.y = 600;
    playerRef.mapsize = mapsize;
    playerRef.entityRef = entities;
    playerRef.windowSize = windowSize;

    let wep = new Bow();
    wep.owner = playerRef;
    playerRef.weapons.push(wep);
    playerRef.canvasRef = canvasRef.current;

    entities.entityList.push(playerRef);

    console.log(playerRef);




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

          const temp = new Spawner();

          temp.x = j * 64;
          temp.y = i * 64;
          temp.entityRef = entities;
          temp.playerRef = playerRef;
          temp.windowSize = windowSize;

          entities.entityList.push(temp);
        }

      }
    }

    //eventek playernek

    document.addEventListener("keydown", (event) => {
      if (event.key === "Escape") {
        paused = !paused;
      }

      playerRef.keyhandler(event)
    });
    document.addEventListener("keyup", (event) => {
      playerRef.keyhandler(event)
    });
    document.addEventListener("mousedown", (event) => {
      playerRef.keyhandler(event)
    });
    document.addEventListener("mouseup", (event) => {
      playerRef.keyhandler(event)
    });
    document.addEventListener("mousemove", (event) => {
      aimpoint = [event.pageX, event.pageY];
      playerRef.aimPoint = aimpoint;
    });

    //console.log(entities)

    let animationFrameId = 0

    let Runtime = 0;
    let lastUpdateTime = 0;

    //Our draw came here
    const render = () => {
      clrCanvas(context);
      Runtime = window.performance.now();
      let deltaTime = (Runtime - lastUpdateTime) / 1000

      if  (playerRef.dead){
        paused=true;
      }

      if (!paused){
        gameTime += deltaTime;
      }
      
      // setting offsets
      renderOffset = [Clamp(playerRef.x - gameSize[0] / 2, 0, (rawmap[0].length * 64) - gameSize[0]), Clamp(playerRef.y - gameSize[1] / 2, 0, (rawmap.length * 64) - gameSize[1])]

      renderOffset = [-renderOffset[0], -renderOffset[1]]

      // tile update
      entities.tileList.forEach(item => {


        item.update(deltaTime, frameCount);
        item.offset = [item.x + renderOffset[0], item.y + renderOffset[1]];
        draw(context, item, item.offset)
      });

      //entity update
      entities.entityList.forEach(item => {
        item.offset = [item.xcenter + renderOffset[0], item.ycenter + renderOffset[1]];
        draw(context, item, item.offset);
        if (item.damagable) {
          drawHPBar(context, item.hpbar, item.offset);
          item.hpbar.setval(item.maxHealth, item.health);
        }

        if(paused){return;}

        item.renderoffset = renderOffset;
        item.Update(deltaTime, frameCount, playerRef);
        

        if (item.dead && item.xpValue != undefined) {

          var xpDrop = new XP();
          xpDrop.value = item.xpValue;
          xpDrop.x = item.x;
          xpDrop.y = item.y;

          entities.entityList.push(xpDrop);

        }
        entities.entityList = entities.entityList.filter((xd) => !xd.dead);

      });



      //projectile update
      entities.projectileList.forEach(item => {

        item.offset = [item.xcenter + renderOffset[0], item.ycenter + renderOffset[1]];
        draw(context, item, item.offset)

        if(paused){return;}


        item.Update(deltaTime, frameCount);

        entities.entityList.forEach(element => {
          if (!element.damagable) { return; }

          if (CheckCollision(item, element) && item.hitlimit > 0) {

            item.hitlimit--;
            element.takeDamage(item);

            if (item.hitlimit <= 0) {
              item.dead = true;
            }

          }
        });

        if (item.x < -500 || item.x > mapsize[0] + 500 || item.y < -500 || item.y > mapsize[1] + 500) {
          item.dead = true;
        }
        entities.projectileList = entities.projectileList.filter((xd) => !xd.dead);

      });

      //effect update
      entities.effectList.forEach(item => {
        item.offset = [item.x + renderOffset[0], item.y + renderOffset[1]];
        drawText(context, item, item.offset);

        if (paused) {return;}


        item.Update(deltaTime, frameCount);
        if (item.frame > item.maxFrame) {
          entities.effectList.splice(item, 1);
        }
      });

      //UI Update

      drawXPBar(context, playerRef)

      //paused UI

      if(paused){
      drawPausedUI(context,playerRef)}

      lastUpdateTime = window.performance.now();
      if (!paused) {
        frameCount++
      }
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