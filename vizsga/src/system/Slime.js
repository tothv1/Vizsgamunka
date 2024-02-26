import { GameLoop } from "react-game-engine";
import { useState } from "react";
import rightIdle from "../Assets/characters/slime.png"
import leftIdle from "../Assets/characters/slime.png"
import { LerpNum } from "./Math";

function Update(deltaTime, frameCount,target) {




  this.frameLength = this.drawing.width / this.width;

  if (this.frameLength > 1) {
    if (frameCount % this.frameDelay === 0) {
      this.frame++;
    }
    if (this.frame >= this.frameLength) {
      this.frame = 0
    }
  }

  this.x=LerpNum(this.x,target.x,1*deltaTime);
  this.y=LerpNum(this.y,target.y,1*deltaTime);
};

const getImg = () =>{

  var temp = new Image();
  temp.src=rightIdle;
  return temp;
}

const Slime = {
  ID : 1,
  frameDelay : 10, //every x updates, the sprite turns over to the next frame
  frameLength : 8, // frames in the spritesheet
  state : "idle",
  mirror : false,

  x : 0,
  y : 0,

  width : 64,
  height : 64,

  speed : 300,
  health : 100,

  frame : 0,

  drawing : getImg(),
  update:Update
}





export { Slime };