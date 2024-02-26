import React, { useRef, useEffect } from 'react'
import { rawMaps } from '../Assets/map/maps';

import { Slime } from '../system/Slime';
import { Wall } from '../system/StoneWall';
import { Player } from '../system/Player';
import '../system/Math';
import { Clamp, Normalise } from '../system/Math';

let renderOffset = [0, 0]

const Canvas = props => {


  let entities = [];

  entities.projectileList = [];
  entities.tileList = [];
  entities.entityList = [];


  const playerRef = Player;
  playerRef.x=600;
  playerRef.y=600;
  playerRef.mapsize = [rawMaps[0][0].length * 64, rawMaps[0].length * 64]
  entities.entityList.push(playerRef);

  const canvasRef = useRef(null)
  let frameCount = 0

  const clrCanvas = (ctx) => {
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)

  }

  const draw = (ctx, object, offset) => {

    ctx.globalCompositeOperation = "source-over"

    ctx.drawImage(object.drawing, object.frame*object.width, 0, object.width, object.height, offset[0], offset[1], object.width, object.height);

  }

  const rawmap = rawMaps[0];
  for (let i = 0; i < rawmap.length; i++) {
    for (let j = 0; j < rawmap[i].length; j++) {
      if (rawmap[i][j] === 1) {
        const temp = Object.create(Wall);
        temp.x=j*64;
        temp.y=i*64;
        entities.tileList.push(temp);
      }
      if (rawmap[i][j] === 2) {
        const temp = Object.create(Slime);
        temp.x=j*64;
        temp.y=i*64;
        entities.entityList.push(temp);
      }
    }
  }
  console.log(entities)
  console.log(playerRef)



  useEffect(() => {

    const canvas = canvasRef.current
    const context = canvas.getContext('2d')

    //console.log(entities)

    let animationFrameId = 0

    let Runtime = 0;
    let lastUpdateTime = 0;

    //Our draw came here
    const render = () => {
      clrCanvas(context);
      Runtime = window.performance.now();
      let deltaTime = (Runtime - lastUpdateTime) / 1000

      entities.entityList.forEach(item => {
        item.update(deltaTime, frameCount,Player);
        if (item.ID===1){
          item.offset = [item.x + renderOffset[0], item.y+ renderOffset[1]];
          draw(context,item,item.offset);
        }
      });

      entities.tileList.forEach(item => {
        item.update(deltaTime, frameCount);
        item.offset = [item.x + renderOffset[0], item.y+ renderOffset[1]];
        draw(context,item,item.offset)
      });

      renderOffset = [Clamp(playerRef.x - window.innerWidth / 2, 0, (rawmap[0].length * 64) - window.innerWidth), Clamp(playerRef.y - window.innerHeight / 2, 0, (rawmap.length * 64) - window.innerHeight)]
      renderOffset = [-renderOffset[0], -renderOffset[1]]

      let playerrenderpos = [playerRef.x + renderOffset[0], playerRef.y + renderOffset[1]]

      draw(context, playerRef, [playerrenderpos[0], playerrenderpos[1]]);

      lastUpdateTime = window.performance.now();
      frameCount++
      animationFrameId = window.requestAnimationFrame(render)
    }
    render()

    return () => {
      window.cancelAnimationFrame(animationFrameId)
    }
  }, [draw])

  return <canvas width={window.innerWidth} height={window.innerHeight} className='mg-0 b-0' ref={canvasRef} {...props} />
}

export default Canvas