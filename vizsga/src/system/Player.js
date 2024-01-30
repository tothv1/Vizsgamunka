import { GameLoop } from "react-game-engine";
import { useState } from "react";
import right from "../Assets/characters/noBKG_KnightRun_strip.png"
import left from "../Assets/characters/noBKG_KnightRun_strip_left.png"
import idle from "../Assets/characters/noBKG_KnightIdle_strip.png"

const ID = 0;

let frameDelay = 10; //every x updates, the sprite turns over to the next frame
let frameLength = 10; // frames in the spritesheet
let state = "idle";
let mirror = false;

let x = 0;
let y = 0;

let width = 96;
const height = 64;

const speed = 300;

let UpState = false;
let DownState = false;
let RightState = false;
let LeftState = false;

document.addEventListener("keydown", keyhandler);
document.addEventListener("keyup", keyhandler);

//irány state, billentyű lenyomás és felengedés alapján
function keyhandler(e) {
  if (e.type === "keydown") {
    if (e.key === "w") UpState = true;
    if (e.key === "s") DownState = true;
    if (e.key === "a") LeftState = true;
    if (e.key === "d") RightState = true;
  }
  if (e.type === "keyup") {
    if (e.key === "w") UpState = false;
    if (e.key === "s") DownState = false;
    if (e.key === "a") LeftState = false;
    if (e.key === "d") RightState = false;
  }
}

let frame = 0;

let drawing = new Image();

function Update(deltaTime, frameCount) {

  let tempRender;



  if (state==="moving"){
    if (mirror){
      drawing.src=left;
      width=96;
    }else{
      drawing.src=right;
      width=96;
    }
  }else{
    drawing.src=idle;
    width=64;
  }


  frameLength=drawing.width/width;




  if (!UpState && !DownState && !LeftState && !RightState) {

    state = "idle";

  } else {
    state = "moving";
  }

  

  if (UpState) {
    y -= speed * deltaTime;
  }
  if (DownState) {
    y += speed * deltaTime;
  }
  if (RightState) {
    x += speed * deltaTime;
    mirror = false;
  }
  if (LeftState) {
    x -= speed * deltaTime;
    mirror = true;
  }


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


export { Update };