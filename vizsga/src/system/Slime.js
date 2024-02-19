import { GameLoop } from "react-game-engine";
import { useState } from "react";
import rightIdle from "../Assets/characters/slime.png"
import leftIdle from "../Assets/characters/slime.png"

const ID = 1;

let frameDelay = 10; //every x updates, the sprite turns over to the next frame
let frameLength = 8; // frames in the spritesheet
let state = "idle";
let mirror = false;

let x=0;
let y=0;

const width = 64;
const height = 64;

const speed = 300;
let health = 100;

let frame = 0;

let drawing = new Image();
drawing.src = rightIdle;

function UpdateSl (deltaTime,frameCount) {

  frameLength=drawing.width/width;

  if (frameLength > 1) {
    if (frameCount % frameDelay === 0) {
      frame++;
    }
    if (frame >= frameLength) {
      frame = 0
    }
  }


  let obj = {
    charID: ID,
    frame: frame * width,
    render: drawing,
    x: x,
    y: y,
    w: width,
    h: height
  }

  return obj;
};

export { UpdateSl };