import { GameLoop } from "react-game-engine";
import { useState } from "react";

let UpState = false;
let DownState = false;
let RightState = false;
let LeftState = false;

document.addEventListener("keydown",keyhandler);
document.addEventListener("keyup",keyhandler);

//irány state, billentyű lenyomás és felengedés alapján
function keyhandler(e){
  if(e.type=="keydown"){
      if (e.key=="w") UpState=true;
      if (e.key=="s") DownState=true;
      if (e.key=="a") LeftState=true;
      if (e.key=="d" ) RightState=true;
  }
  if(e.type=="keyup"){
      if (e.key=="w" ) UpState=false;
      if (e.key=="s" ) DownState=false;
      if (e.key=="a" ) LeftState=false;
      if (e.key=="d") RightState=false;
  }
}


function MoveBox  (entities, { input, time }) {
  const deltaTime = time.delta / 1000;

  const speed = 300;

  
  
  //-- I'm choosing to update the game state (entities) directly for the sake of brevity and simplicity.
  //-- There's nothing stopping you from treating the game state as immutable and returning a copy..
  //-- Example: return { ...entities, t.id: { UPDATED COMPONENTS }};
  //-- That said, it's probably worth considering performance implications in either case.
  const box1 = entities["box1"];
  const { payload } = input.find(x => x.name) || {};

  if(UpState){
    box1.y-=speed*deltaTime;
  }
  if(DownState){
    box1.y+=speed*deltaTime;
  }
  if(RightState){
    box1.x+=speed*deltaTime;
  }
  if(LeftState){
    box1.x-=speed*deltaTime;
  }

  

  return entities;
};

export { MoveBox };