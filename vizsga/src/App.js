import React, { PureComponent, useState } from "react";
import { GameEngine } from "react-game-engine";
import Canvas from "./render/canvas";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Menu from "./menu";
import AuthPage from "./AuthPage";
import Login from "./login";

import wall from "./Assets/map/wall.png";
import karakter from "./Assets/characters/hatternelkuli.png";
import slime from "./Assets/characters/slime.png";

import { Update } from "./system/Player";
import { UpdateT } from "./system/StoneWall";
import { UpdateSl } from "./system/Slime";
import { rawMaps } from "./Assets/map/maps";


export default function App() {
  //EZT RAKD FEL
  //npm install --save react-game-engine

  const [renderOffset, setRenderOffset] = useState([]);

  return (
      <Router>
        <Routes>
          <Route path="/" element={<AuthPage />} />
          <Route path="/menu" element={<Menu />} />
          <Route path="/login" element={<Login />} />
          <Route
            path="/game"
            element={
              <Canvas
                style={{
                  width: window.innerWidth,
                  height: window.innerHeight,
                  backgroundColor: "lightblue",
                }}
                offset={{ renderOffset, setRenderOffset }}
              />
            }
          />
        </Routes>
      </Router>

  );
}
