import { GameLoop } from "react-game-engine";
import { useState } from "react";
import render from "../Assets/characters/hatternelkuli.png"

const ID = 0;

let x=0;
let y=0;

const width = 128;
const height = 128;

const speed = 300;

let UpState = false;
let DownState = false;
let RightState = false;
let LeftState = false;

document.addEventListener("keydown",keyhandler);
document.addEventListener("keyup",keyhandler);

//irány state, billentyű lenyomás és felengedés alapján
function keyhandler(e){
  if(e.type==="keydown"){
      if (e.key==="w") UpState=true;
      if (e.key==="s") DownState=true;
      if (e.key==="a") LeftState=true;
      if (e.key==="d" ) RightState=true;
  }
  if(e.type==="keyup"){
      if (e.key==="w" ) UpState=false;
      if (e.key==="s" ) DownState=false;
      if (e.key==="a" ) LeftState=false;
      if (e.key==="d") RightState=false;
  }
}


function Update (deltaTime) {


  
  
  //-- I'm choosing to update the game state (entities) directly for the sake of brevity and simplicity.
  //-- There's nothing stopping you from treating the game state as immutable and returning a copy..
  //-- Example: return { ...entities, t.id: { UPDATED COMPONENTS }};
  //-- That said, it's probably worth considering performance implications in either case.

  if(UpState){
    y-=speed*deltaTime;
  }
  if(DownState){
    y+=speed*deltaTime;
  }
  if(RightState){
    x+=speed*deltaTime;
  }
  if(LeftState){
    x-=speed*deltaTime;
  }

  let obj ={
    charID:ID,
    render:render,
    x:x,
    y:y,
    w:width,
    h:height
  }

  return obj;
};

export { Update };