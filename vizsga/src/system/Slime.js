import { GameLoop } from "react-game-engine";
import { useState } from "react";
import rightIdle from "../Assets/characters/slime.png"
import leftIdle from "../Assets/characters/slime.png"
import { GetDirAngle, GetDirection, LerpNum, Normalise, RadToDegrees, Translate } from "./Math";
import { HPbar } from "../render/HPBar";

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

  let dir = GetDirection([this.x,this.y],[target.x,target.y]);
  let normDir = Normalise(dir);
  let translation = Translate([this.x,this.y],[normDir[0]*this.speed*deltaTime,normDir[1]*this.speed*deltaTime]);

  this.x=translation[0];
  this.y=translation[1];

  this.xcenter = this.x-(this.width/2);
  this.ycenter = this.y-(this.height/2);

};

function takeDamage(damage){
  this.health-=damage;
  if (this.health<=0){
    this.dead=true;
  }
}

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

  dead:false,
  
  rotation : 0,

  x : 0,
  y : 0,

  team:2,

  maxHealth : 100,
  health : 100,

  width : 64,
  height : 64,

  xhitbox:32,
  yhitbox:32,

  speed : 300,
  health : 100,

  frame : 0,

  xcenter : 0,
  ycenter : 0,

  drawing : getImg(),
  update:Update,
  takeDamage:takeDamage,
  hpbar : Object.create(HPbar)

}





export { Slime };