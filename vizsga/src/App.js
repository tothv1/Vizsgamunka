import React, { PureComponent } from "react";
import { GameEngine } from "react-game-engine";
import { Box, Wall } from "./render/renderers";
import { MoveBox } from "./system/Player";
import Canvas from "./render/canvas";

import wall from "./Assets/map/wall.png"
import karakter from "./Assets/characters/hatternelkuli.png"
import slime from "./Assets/characters/slime.png";

export default class SimpleGame extends PureComponent {
  render() {
    //EZT RAKD FEL
    //npm install --save react-game-engine
    const rawMap = [
      [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
      [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,0,1,2,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
      [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
    ];

    return (
      <div>
        <Canvas style={{width:window.innerWidth, height:window.innerHeight, backgroundColor: "lightblue" }}
        systems={[MoveBox]}
        entities={{
          //-- Notice that each entity has a unique id (required)
          //-- and a renderer property (optional). If no renderer
          //-- is supplied with the entity - it won't get displayed.
          wall: {render: wall, x: 0, y: 0, w:64, h:64, rawMap:rawMap},
          player: {render:karakter, x: 200,  y: 200, w:64, h:64 },
          slime: {render:slime, x: 200,  y: 200, w:64, h:64 }
        }}/>
      </div>

      

      /*<GameEngine
        style={{ width: rawMap[0].length*64, height: rawMap.length*64, backgroundColor: "lightblue" }}
        systems={[MoveBox]}
        entities={{
          //-- Notice that each entity has a unique id (required)
          //-- and a renderer property (optional). If no renderer
          //-- is supplied with the entity - it won't get displayed.
          wall: { x: 0, y: 0, renderer: <Wall map={rawMap}/>},
          box1: { x: 200,  y: 200, windowWidth:rawMap.length*64, windowHeight:rawMap[0].length*64, renderer: <Box  />}
        }}>

      </GameEngine>*/
    );
  }
}