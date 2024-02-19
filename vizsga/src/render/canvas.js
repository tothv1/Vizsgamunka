import React, { useRef, useEffect } from 'react'
import { rawMaps } from '../Assets/map/maps';

import { UpdateSl } from '../system/Slime';
import { UpdateT } from '../system/StoneWall';
import { Update } from '../system/Player';
import '../system/Math';
import { Clamp, Normalise } from '../system/Math';

let renderOffset = [0,0]

const Canvas = props => {



  let entities = {};

  entities.terrain={
    rawmap:rawMaps,
    update:UpdateT
  }

  entities.player={
    update:Update
  }

  entities.slime={
    update:UpdateSl
  }


  console.log(entities)

  const canvasRef = useRef(null)
  let frameCount = 0


  const clrCanvas = (ctx) => {
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)

  }

  const draw = async (ctx, object,offset) => {

    //console.log(object)
    ctx.drawImage(object.render, object.frame, 0, object.w, object.h, offset[0], offset[1], object.w, object.h);

  }

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

      const map = entities.terrain;
      let player = entities.player.update(deltaTime,frameCount)

      //const slime = entities.slime;

      const rawmap = map.rawmap[0];

      console.log(`x: ${player.x}\ny: ${player.y}`)
    

      for (let i = 0; i < rawmap.length; i++) {
        for (let j = 0; j < rawmap[i].length; j++) {
          if (rawmap[i][j] === 1) {

            let obj = UpdateT(deltaTime,frameCount);
            obj.x = j * 64;
            obj.y = i * 64;
            obj.offset=[j*64+renderOffset[0],i*64+renderOffset[1]]

            draw(context, obj,obj.offset)
          }
          if (rawmap[i][j] === 2) {

            let obj = UpdateSl();
            obj.x = j * 64;
            obj.y = i * 64;
            obj.offset=[j*64+renderOffset[0],i*64+renderOffset[1]]

            draw(context, obj,obj.offset)
          }
        }
      }



      renderOffset=[-player.x,-player.y]

      renderOffset[0] = Clamp(renderOffset[0],0,5000)
      renderOffset[1] = Clamp(renderOffset[1],0,5000)

      console.log(Normalise(renderOffset))

      

      draw(context, player,[window.innerWidth/2,window.innerHeight/2]);


      
      

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