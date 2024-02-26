import { GameLoop } from "react-game-engine";
import { useState } from "react";
import rightIdle from "../Assets/characters/slime.png"
import leftIdle from "../Assets/characters/slime.png"

function Update(deltaTime, frameCount) {

  const ref = this;

  ref.frameLength = ref.drawing.width / ref.width;

  if (ref.frameLength > 1) {
    if (ref.frameCount % ref.frameDelay === 0) {
      ref.frame++;
    }
    if (ref.frame >= ref.frameLength) {
      ref.frame = 0
    }
  }

};

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

  drawing : new Image().src=rightIdle,
  update:Update
}





export { Slime };