import { GameLoop } from "react-game-engine";
import { useState } from "react";
import render from "../Assets/map/tiles/ezegyfal.jpg"

const getImg = () =>{

  var temp = new Image();
  temp.src=render;
  return temp;
}




const Wall = {

  ID : 1,
  state : "Sleep",

  frameDelay : 10, //every x updates, the sprite turns over to the next frame
  frameLength : 1, // frames in the spritesheet
  mirror : false,

  x:0,
  y:0,
  rotation : 0,

  offset:[],
  
  width : 64,
  height : 64,
  
  frame : 0,
  
  
  drawing : getImg(),
  
  update:Update,

}




function Update (deltaTime, frameCount) {

  this.frameLength=this.drawing.width/this.width;


  if (this.frameLength > 1) {
    if (this.frameCount % this.frameDelay === 0) {
      this.frame++;
    }
    if (this.frame >= this.frameLength) {
      this.frame = 0
    }
  } 

};

export { Wall };