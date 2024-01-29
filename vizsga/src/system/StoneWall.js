import { GameLoop } from "react-game-engine";
import { useState } from "react";
import render from "../Assets/map/tiles/row-4-column-6.png"

let ID = 1;
let state = "sleep"

let x=0;
let y=0;

const width = 64;
const height = 64;

function UpdateT (deltaTime) {


  let obj ={
    render:render,
    x:x,
    y:y,
    w:width,
    h:height
  }

  return obj;
};

export { UpdateT };