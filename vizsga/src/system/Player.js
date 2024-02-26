import { GameLoop } from "react-game-engine";
import { useState } from "react";
import right from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-right.png"
import left from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-left.png"
import up from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-up.png"
import down from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-down.png"

import idle from "../Assets/characters/noBKG_KnightIdle_strip.png"
import "../system/Math";
import { Clamp } from "../system/Math";

const Player = {
  ID : 0,

  frameDelay : 20, //every x updates, the sprite turns over to the next frame
  frameLength : 10, // frames in the spritesheet
  state : "idle",
  direction : "none",

  x : 0,
  y : 0,

  mapsize : [0,0],
  offset : [0,0],

  width : 39,
  height : 64,
  
  speed : 300,
  health : 100,

  UpState : false,
  DownState : false,
  RightState : false,
  LeftState : false,

  frame : 0,

  drawing : new Image(),
  update:Update

}




document.addEventListener("keydown", keyhandler);
document.addEventListener("keyup", keyhandler);

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
      console.log(this)
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
      this.width = 39;
    }
    if (this.direction === "right") {
      this.drawing.src = right;
      this.width = 39;
    }
    if (this.direction === "up") {
      this.drawing.src = up;
      this.width = 53;
    }
    if (this.direction === "down") {
      this.drawing.src = down;
      this.width = 53;
    }

    if (this.frameCount % this.frameDelay === 0) {
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


  this.x = Clamp(this.x, 0, this.mapsize[0]);
  this.y = Clamp(this.y, 0, this.mapsize[1]);

};

export { Player };