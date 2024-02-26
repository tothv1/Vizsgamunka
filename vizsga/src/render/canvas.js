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
  entities.entityList.push(Slime);




  const playerRef = Player;
  playerRef.mapsize = [rawMaps[0][0].length * 64, rawMaps[0].length * 64]
  entities.entityList.push(playerRef);

  const canvasRef = useRef(null)
  let frameCount = 0


  const clrCanvas = (ctx) => {
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)

  }

  const draw = async (ctx, object, offset) => {

    ctx.globalCompositeOperation = "source-over"


    ctx.drawImage(object.drawing, object.frame, 0, object.width, object.height, object.offset[0], object.offset[1], object.width, object.height);

  }

  const rawmap = rawMaps[0];

  console.log(rawmap);

  for (let i = 0; i < rawmap.length; i++) {
    for (let j = 0; j < rawmap[i].length; j++) {
      if (rawmap[i][j] === 1) {


        const temp = Wall;
        console.log(temp)
        temp.x = j * 64;
        temp.y = i * 64;
        console.log(temp)
        entities.tileList.push(temp)
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
        item.update(deltaTime, frameCount);
        
      });

      entities.tileList.forEach(item => {
        item.update(deltaTime, frameCount);
        item.offset = [item.x + renderOffset[0], item.y+ renderOffset[1]];
        draw(context,item,renderOffset)
      });



      renderOffset = [Clamp(playerRef.x - window.innerWidth / 2, 0, (rawmap[0].length * 64) - window.innerWidth), Clamp(playerRef.y - window.innerHeight / 2, 0, (rawmap.length * 64) - window.innerHeight)]
      renderOffset = [-renderOffset[0], -renderOffset[1]]

      //console.log(`render\nx: ${renderOffset[0]}\ny: ${renderOffset[1]}`)
      //console.log(`player\nx: ${player.x}\ny: ${player.y}`)

      let playerrenderpos = [playerRef.x + renderOffset[0], playerRef.y + renderOffset[1]]

      //console.log(`should be here\nx: ${playerrenderpos[0]}\ny: ${playerrenderpos[1]}`)

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