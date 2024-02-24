import { GameLoop } from "react-game-engine";
import { useState } from "react";
import render from "../Assets/map/tiles/ezegyfal.jpg"

let ID = 2;
let state = "sleep"

let frameDelay = 10; //every x updates, the sprite turns over to the next frame
let frameLength = 1; // frames in the spritesheet
let mirror = false;

let x=0;
let y=0;

const width = 64;
const height = 64;

let frame = 0;


let drawing = new Image();
drawing.src = render;

function UpdateT (deltaTime, frameCount) {

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
    ID: ID,
    frame: frame * width,
    render: drawing,
    x: x,
    y: y,
    w: width,
    h: height
  }

  return obj;
};

export { UpdateT };