import { GameLoop } from "react-game-engine";
import { useState } from "react";

let UpState = false;
let DownState = false;
let RightState = false;
let LeftState = false;
function MoveBox  (entities, { input, time }) {
  const deltaTime = time.delta / 1000;

  const speed = 300;


  
  //-- I'm choosing to update the game state (entities) directly for the sake of brevity and simplicity.
  //-- There's nothing stopping you from treating the game state as immutable and returning a copy..
  //-- Example: return { ...entities, t.id: { UPDATED COMPONENTS }};
  //-- That said, it's probably worth considering performance implications in either case.
  const box1 = entities["box1"];
  const { payload } = input.find(x => x.name) || {};

  if (payload!==undefined){

  if (payload._reactName==="onKeyDown") {
    switch (payload.key) {
      case ("w" || "W" || "ArrowUp"):
        UpState=true;
        break;
      case "s":
        DownState=true;
        break;
      case "a":
        LeftState=true;
        break;
      case "d":
        RightState=true;
        break;
      default:
        break;
    }
  }

  if (payload._reactName==="onKeyUp") {
    switch (payload.key) {
      case ("w" || "W" || "ArrowUp"):
        UpState=false;
        break;
      case "s":
        DownState=false;
        break;
      case "a":
        LeftState=false;
        break;
      case "d":
        RightState=false;
        break;
      default:
        break;
    }
  }}

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