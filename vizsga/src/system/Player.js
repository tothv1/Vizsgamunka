import { GameLoop } from "react-game-engine";
import { useState } from "react";
import right from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-right.png"
import left from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-left.png"
import up from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-up.png"
import down from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-down.png"

import idle from "../Assets/characters/noBKG_KnightIdle_strip.png"
import "../system/Math";
import { Clamp, Normalise,GetDirection, GetDirRad, getRandomRange,CreateProjectile } from "../system/Math";
import { Slime } from "./Slime";
import { Arrow } from "./Projectile";

const Player = {
  ID : 0,

  frameDelay : 20, //every x updates, the sprite turns over to the next frame
  frameLength : 10, // frames in the spritesheet
  state : "idle",
  direction : "down",

  rotation : 0,

  x : 1,
  y : 0,

  xcenter : 0,
  ycenter : 0,

  renderx:0,
  rendery:0,

  team:1,

  xhitbox:32,
  yhitbox:32,

  mapsize : [0,0],
  offset : [0,0],
  renderoffset : [0,0],

  width : 64,
  height : 64,
  
  speed : 300,
  
  health : 100,

  UpState : false,
  DownState : false,
  RightState : false,
  LeftState : false,

  frame : 0,

  canvasRef :0,

  drawing : new Image(),
  entityRef:[],
  update : Update,
  keyhandler : keyhandler,
  takeDamage:takeDamage,
  shoot : shoot

}


function shoot(e,obj,aimOffset){
  
  let direction = GetDirection([obj.x, obj.y], [e.pageX - obj.renderoffset[0]-aimOffset[0], e.pageY - obj.renderoffset[1]-aimOffset[1]])
  let normalised = Normalise(direction);

  let xd = Object.create(Arrow);

  let temp = CreateProjectile([obj.x,obj.y], normalised, xd,this.team);
  return temp
}


//irány state, billentyű lenyomás és felengedés alapján
function keyhandler(e) {



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


function Update(deltaTime, frameCount) {

  if (this.state === "moving") {
    if (this.direction === "left") {
      this.drawing.src = left;
    }
    if (this.direction === "right") {
      this.drawing.src = right;
    }
    if (this.direction === "up") {
      this.drawing.src = up;
    }
    if (this.direction === "down") {
      this.drawing.src = down;
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


  this.x = Clamp(this.x, 0, this.mapsize[0]+this.width);
  this.y = Clamp(this.y, 0, this.mapsize[1]+this.height);

  this.xcenter = this.x-this.width/2;
  this.ycenter = this.y-this.height/2;

};

function takeDamage(damage){
  this.health-=damage;
  if (this.health<=0){
    this.dead=true;
  }
}

export { Player };