import React, { useRef, useEffect, useState } from 'react'
import { rawMaps } from '../Assets/map/maps';

import { Slime, xd } from '../system/Slime';
import { Wall } from '../system/StoneWall';
import { Tile } from '../Assets/background/tiletile.jpg'
import { Player } from '../system/Player';
import { DMGpopup } from './DmgPopup';
import '../system/Math';
import { Clamp, Normalise, CheckCollision, getRandomRange, CheckInside, randomTest } from '../system/Math';
import { Bow } from '../system/Weapons/Bow';
import { useNavigate } from 'react-router-dom';

import { XP } from '../system/Pickups/Experience';
import { Spawner } from '../system/Spawner';
import { ItemCard } from '../system/PassiveItems/ItemCard';
import { CardPool } from '../system/PassiveItems/CardPool';

import Bow1 from '../Assets/weapon/BOW1.png';
import Crossbow from '../Assets/weapon/CROSSBOW.png';
import Icerod from '../Assets/weapon/ICEROD.png';

import { CButton, QuitButton } from '../system/CButton';

const Canvas = props => {
  
  let renderOffset = [0, 0]
  let gameSize = [0, 0]
  let windowSize = [0, 0];
  
  let aimpoint = [0, 0];
  let entities = [];

  const canvasRef = useRef(null)
  gameSize = [props.style.width, props.style.height]
  let frameCount = 0;

  const navigate = useNavigate();

  let gameTime = 0;
  let paused = false;
  let pauseBlock = false;
  let pausedManual = false;

  let lvlUpCards = [];
  let selectedCard = null;

  let buttons = [];
  let selectedButton = null;

  let enemyScale = 1;

  function setPause(state) {
    paused = state;
  }

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

    let lock = false;

    ctx.globalAlpha = 0.7;

    ctx.fillStyle = 'black';
    ctx.beginPath();
    ctx.rect(0, 0, ctx.canvas.width, ctx.canvas.height);
    ctx.closePath();
    ctx.fill();

    ctx.globalAlpha = 1.0;

    ctx.font = `50px Joystix Monospace`;

    ctx.fillStyle = 'white';
    ctx.fillText(`PAUSED`, 475, 150);

    ctx.strokeStyle = 'black';
    ctx.strokeText(`PAUSED`, 475, 150);

    if (pausedManual) {


      for (let index = 0; index < buttons.length; index++) {
        ctx.lineWidth = 1;

        if (CheckInside([aimpoint[0] - windowSize[0], aimpoint[1] - windowSize[1]], buttons[index])) {



          ctx.lineWidth = 10;
          ctx.strokeStyle = 'yellow';

          ctx.beginPath();
          ctx.strokeRect(buttons[index].xOffset, buttons[index].yOffset, buttons[index].width, buttons[index].height)
          ctx.closePath();
          ctx.fill();

          if (!lock) {
            selectedButton = buttons[index];
            lock = true;
          }
          ctx.lineWidth = 1;
        } else {
          selectedButton = null;
        }


        ctx.fillStyle = 'gray';
        ctx.beginPath();
        ctx.rect(buttons[index].xOffset, buttons[index].yOffset, buttons[index].width, buttons[index].height);
        ctx.closePath();
        ctx.fill();

        ctx.font = `${buttons[index].height / 2}px Joystix Monospace`;


        ctx.fillStyle = 'white';
        ctx.fillText(`MAIN MENU`, buttons[index].xOffset + 6, buttons[index].yOffset + buttons[index].height - 15);
        ctx.strokeStyle = 'black';
        ctx.strokeText(`MAIN MENU`, buttons[index].xOffset + 6, buttons[index].yOffset + buttons[index].height - 15);
      }

    }
    ctx.restore();
  }

  const drawItemPickUI = (ctx, cards, statcard) => {

    ctx.save();

    let cardLock = false;

    ctx.globalAlpha = 1.0;

    ctx.font = `15px Joystix Monospace`;

    ctx.fillStyle = 'white';
    ctx.fillText(`ATK: ${Math.floor(statcard.DamageMult * 100)}%`, 50, 300);
    ctx.strokeStyle = 'black';
    ctx.strokeText(`ATK: ${Math.floor(statcard.DamageMult * 100)}%`, 50, 300);

    ctx.fillStyle = 'white';
    ctx.fillText(`HP: ${Math.floor(statcard.MaxHealth)}`, 50, 315);
    ctx.strokeStyle = 'black';
    ctx.strokeText(`HP: ${Math.floor(statcard.MaxHealth)}`, 50, 315);

    ctx.fillStyle = 'white';
    ctx.fillText(`CC: ${Math.floor(statcard.critChance)}%`, 50, 330);
    ctx.strokeStyle = 'black';
    ctx.strokeText(`CC: ${Math.floor(statcard.critChance)}%`, 50, 330);

    ctx.fillStyle = 'white';
    ctx.fillText(`CD: ${Math.floor(statcard.critDamageMult * 100)}%`, 50, 345);
    ctx.strokeStyle = 'black';
    ctx.strokeText(`CD: ${Math.floor(statcard.critDamageMult * 100)}%`, 50, 345);

    ctx.fillStyle = 'white';
    ctx.fillText(`AS: ${Math.floor(statcard.FirerateMult * 100)}%`, 50, 360);
    ctx.strokeStyle = 'black';
    ctx.strokeText(`AS: ${Math.floor(statcard.FirerateMult * 100)}%`, 50, 360);

    for (let index = 0; index < 3; index++) {


      if (CheckInside([aimpoint[0] - windowSize[0], aimpoint[1] - windowSize[1]], cards[index])) {

        ctx.lineWidth = 10;
        ctx.strokeStyle = 'yellow';

        ctx.beginPath();
        ctx.strokeRect(cards[index].xOffset, cards[index].yOffset, cards[index].width, cards[index].height)
        ctx.closePath();
        ctx.fill();

        if (!cardLock) {
          selectedCard = cards[index];
          cardLock = true;
        }

        ctx.lineWidth = 1;

      } else if (!cardLock) {
        selectedCard = null;
      }
      ctx.lineWidth = 1;

      ctx.fillStyle = 'gray';
      ctx.beginPath();
      ctx.rect(cards[index].xOffset, cards[index].yOffset, cards[index].width, cards[index].height);
      ctx.closePath();
      ctx.fill();

      ctx.drawImage(cards[index].icon, cards[index].xOffset + 10, cards[index].yOffset + 10);

      ctx.font = `15px Joystix Monospace`;

      ctx.fillStyle = 'white';
      ctx.fillText(`${cards[index].card.Description}`, cards[index].xOffset + 80, cards[index].yOffset + 30);

      ctx.strokeStyle = 'black'
      ctx.strokeText(`${cards[index].card.Description}`, cards[index].xOffset + 80, cards[index].yOffset + 30);

    }

    ctx.restore();

  }

  //inventory elhelyezése
  const drawItemInInventory = (ctx, itemImage, slotIndex) => {
    const inventoryWidth = 500;
    const inventoryHeight = 700;

    itemImage.width = 64;
    itemImage.height = 64;

    ctx.drawImage(itemImage, inventoryWidth, inventoryHeight + slotIndex * 64);
  };


  useEffect(() => {

    const canvas = canvasRef.current
    const context = canvas.getContext('2d')
    var rect = canvas.getBoundingClientRect();
    windowSize = [rect.left, rect.top]

    //item rajzolása az első slotba
    const itemImage = new Image();
    itemImage.src = Bow1;



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
    playerRef.tokenData = props.tokendata;
    playerRef.GameStatCard.userid = props.tokendata.userId;
    playerRef.SetPause = setPause;

    const spawnerRef = new Spawner();
    spawnerRef.x = 0;
    spawnerRef.y = 0;
    spawnerRef.entityRef = entities;
    spawnerRef.playerRef = playerRef;
    spawnerRef.windowSize = windowSize;

    entities.entityList.push(spawnerRef);


    let wep = new Bow();
    wep.owner = playerRef;
    playerRef.weapons.push(wep);
    playerRef.canvasRef = canvasRef.current;

    entities.entityList.push(playerRef);

    playerRef.RecalcStats();

    console.log(playerRef);

    let quit = new QuitButton();
    quit.xOffset = 500;
    quit.yOffset = 300;
    quit.height = 50;
    quit.width = 200;
    buttons.push(quit);

    const rawmap = rawMaps[0];


    for (let i = 0; i < rawmap.length; i++) {
      for (let j = 0; j < rawmap[i].length; j++) {
        if (rawmap[i][j] === 1) {
          const temp = Object.create(Wall);
          temp.x = j * 64;
          temp.y = i * 64;

          entities.tileList.push(temp);
        }
      }
    }

    //eventek playernek

    document.addEventListener("keydown", (event) => {
      if ((event.key === "Escape" || event.key === "p") && !pauseBlock) {
        pausedManual = !pausedManual;
        paused = !paused;
      }
      playerRef.keyhandler(event)
    });
    document.addEventListener("keyup", (event) => {
      playerRef.keyhandler(event)
      playerRef.ItemPick(event);
    });
    document.addEventListener("mousedown", (event) => {
      playerRef.keyhandler(event)

      if (selectedCard != null) {
        playerRef.ItemPick(event, selectedCard);
        playerRef.RecalcStats();
      }

      if (selectedButton != null) {
        if (selectedButton.role === "quit") {
          selectedButton = null;
          navigate("/");
        }
      }
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

      enemyScale=gameTime/100;
      spawnerRef.enemyScale=enemyScale;

      if (playerRef.dead) {
        paused = true;
      }

      if (!paused) {
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
          if (item.StatCard != undefined) {
            item.hpbar.setval(item.StatCard.MaxHealth, item.StatCard.Health);

          } else {
            item.hpbar.setval(item.maxHealth, item.health);
          }
        }

        if (paused) { return; }

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

        if (paused) { return; }


        item.Update(deltaTime, frameCount);

        entities.entityList.forEach(element => {
          if (!element.damagable) { return; }

          if (CheckCollision(item, element) && item.hitlimit > 0 && !item.Hits.includes(element)) {

            item.hitlimit--;
            element.takeDamage(item);
            item.Hits.push(element);

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

        if (paused) { return; }


        item.Update(deltaTime, frameCount);
        if (item.frame > item.maxFrame) {
          entities.effectList.splice(item, 1);
        }
      });





      drawItemInInventory(context, itemImage, 0);
      //paused UI

      if (paused) {
        drawPausedUI(context, playerRef)
      }

      //UI Update

      drawXPBar(context, playerRef)

      //Item select UI
      if (playerRef.ItemPicks > 0) {

        if (lvlUpCards.length == 0) {
          for (let i = 0; i < 3; i++) {
            let pool = new CardPool().card;

            let roll = Math.floor(Math.random() * pool.length);

            let xd = new ItemCard();
            xd = pool[roll];

            xd.level=2;

            if(!xd.card.statcard){
              xd.initl(2);
            }
            
            xd.init();

            console.log(xd)

            xd.yOffset = 200 + (xd.height + 30) * i;

            lvlUpCards.push(xd);
          }
          playerRef.LVLUpCards = lvlUpCards;
        }

        pauseBlock = true;
        paused = true;
        drawItemPickUI(context, lvlUpCards, playerRef.StatCard)
      } else {
        lvlUpCards = []
        pauseBlock = false;
      }

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
    <div className={"d-flex align-items-center justify-content-center vh-100 tileHatter"}>
      <canvas width={gameSize[0]} height={gameSize[1]} className='mg-0 b-0' ref={canvasRef} {...props} />
    </div>
  )
}

export default Canvas