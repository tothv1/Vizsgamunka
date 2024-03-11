import { GameLoop } from "react-game-engine";
import { useState } from "react";
import right from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-right.png"
import left from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-left.png"
import up from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-up.png"
import down from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-down.png"

import "../system/Math";
import { Clamp } from "../system/Math";
import { HPbar } from "../render/HPBar";
import { StatCard } from "./StatCard";
import { Bow } from "./Weapons/Bow";

class Player {
  ID = 0;

  level=1;
  currrentXP = 0;
  requiredXP = 10;
  XPRatio = 0;

  frameDelay = 20; //every x updates; the sprite turns over to the next frame
  frameLength = 10; // frames in the spritesheet
  state = "moving";
  direction = "right";

  width = 64;
  height = 64;

  rotation = 0;

  x = 1;
  y = 0;
  xcenter = 0;
  ycenter = 0;
  xhitbox = 32;
  yhitbox = 32;

  mapsize = [0, 0];
  offset = [0, 0];
  renderoffset = [0, 0];

  statcard = new StatCard();

  speed = 300;

  maxHealth = 10000;
  health = 10000;

  team = 1;
  shooting = false;
  aimPoint = [0, 0];
  windowSize = [0, 0];
  weapons = [];

  damagable=true;

  UpState = false;
  DownState = false;
  RightState = false;
  LeftState = false;

  frame = 0;

  canvasRef = 0;

  drawing = new Image();
  entityRef = [];

  hpbar = Object.create(HPbar)

  Update(deltaTime, frameCount) {

    if (this.currrentXP>=this.requiredXP){
      
      this.currrentXP-=this.requiredXP;
      this.level+=1;

      let obj = new Bow();
      obj.owner=this;


      this.weapons.push(obj)

      this.requiredXP=Math.floor(this.requiredXP*1.1);    //exponential scale
      this.requiredXP=+this.level*10;                     //linear scale
    }

    this.XPRatio = this.currrentXP/this.requiredXP;
    

    if (this.state === "moving") {
      if (this.direction === "left") {
        this.drawing.src = left;
      }
      if (this.direction === "right") {
        this.drawing.src = right;
      }
      if (this.direction === "up") {
        //this.drawing.src = up;
      }
      if (this.direction === "down") {
        //this.drawing.src = down;
      }

      if (frameCount % this.frameDelay === 0) {
        this.frame++;
      }
      if (this.frame >= this.frameLength) {
        this.frame = 0
      }
    } else {
      this.frame = 0;
      //drawing.src=idle;
      //width=64;
    }

    this.frameLength = this.drawing.width / this.width;

    if (!this.UpState && !this.DownState && !this.LeftState && !this.RightState) {
      this.state = "idle";
    } else {
      this.state = "moving";
    }

    //irány state, lezárja az irányt

    if (this.UpState) {
      this.y -= this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "up";
      }
    }

    if (this.DownState) {
      this.y += this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "down";
      }
    }

    if (this.RightState) {
      this.x += this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "right";
      }
    }

    if (this.LeftState) {
      this.x -= this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "left";
      }
    }

    this.x = Clamp(this.x, 0, this.mapsize[0] + this.width);
    this.y = Clamp(this.y, 0, this.mapsize[1] + this.height);

    this.xcenter = this.x - this.width / 2;
    this.ycenter = this.y - this.height / 2;

    this.weapons.forEach(weapon => {
      weapon.Update(deltaTime, frameCount)
    });


  };
  takeDamage(damage) {
    this.health -= damage;
    if (this.health <= 0) {
      this.dead = true;
    }
  }
  keyhandler(e) {

    if (e.type === "mouseup") {
      this.weapons.forEach(weapon => {
        weapon.active = false;
      });
    }
    if (e.type === "mousedown") {
      this.weapons.forEach(weapon => {
        weapon.active = true;
      });
    }

    if (e.type === "keydown") {
      if (e.key === "w") {

        this.UpState = true;
      }
      if (e.key === "s") {

        this.DownState = true;
      }
      if (e.key === "a") {

        this.LeftState = true;
      }
      if (e.key === "d") {

        this.RightState = true;
      }
    }

    // felengedésen kinyitja az irány lock-ot, rendereléshez kell
    if (e.type === "keyup") {
      if (e.key === "w") {
        this.UpState = false;
        if (this.direction === "up") this.direction = "none";
      }
      if (e.key === "s") {
        this.DownState = false;
        if (this.direction === "down") this.direction = "none";
      }
      if (e.key === "a") {
        this.LeftState = false;
        if (this.direction === "left") this.direction = "none";
      }
      if (e.key === "d") {
        this.RightState = false;
        if (this.direction === "right") this.direction = "none";
      }
    }
  }
}

//irány state, billentyű lenyomás és felengedés alapján




export { Player };