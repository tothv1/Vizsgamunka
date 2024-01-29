import { GameLoop } from "react-game-engine";
import { useState } from "react";
import render from "../Assets/characters/slime.png"

const ID = 1;

let x=0;
let y=0;

const width = 64;
const height = 64;

const speed = 300;


function UpdateSl (deltaTime) {



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

export { UpdateSl };