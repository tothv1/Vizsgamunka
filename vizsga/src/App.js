import React, { PureComponent, useState } from "react";
import { GameEngine } from "react-game-engine";
import Canvas from "./render/canvas";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Menu from "./menu";

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
          <Route path="/" element={<Menu />} />
          <Route
            path="/gam"
            element={
              <Canvas
                style={{
                  width: window.innerWidth,
                  height: window.innerHeight,
                  backgroundColor: "lightblue",
                }}
                offset={{ renderOffset, setRenderOffset }}
                entities={{
                  //-- Notice that each entity has a unique id (required)
                  //-- and a renderer property (optional). If no renderer
                  //-- is supplied with the entity - it won't get displayed.
                  terrain: { update: UpdateT, rawMap: rawMaps },
                  player: { update: Update },
                  slime: { update: UpdateSl },
                }}
              />
            }
          />
        </Routes>
      </Router>

  );
}
