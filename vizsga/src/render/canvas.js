import React, { useRef, useEffect } from 'react'
import { rawMaps } from '../Assets/map/maps';

import { UpdateSl } from '../system/Slime';
import { UpdateT } from '../system/StoneWall';
import { Update } from '../system/Player';
import '../system/Math';
import { Clamp, Normalise } from '../system/Math';

let renderOffset = [0,0]
let gamepad



window.addEventListener("gamepadconnected", (e) => {
  gamepad = navigator.getGamepads()[e.gamepad.index];
  console.log(gamepad)


  setInterval(function(){
    console.log(gamepad.buttons[6].pressed)
  }, 100)
});



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

    ctx.globalCompositeOperation =     "source-over"

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

      if(gamepad!=undefined){
        

      }

      const rawmap = map.rawmap[0];

      player.mapsize=[rawmap[0].length*64,rawmap.length*64];
    

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

      

      renderOffset=[Clamp(player.x-window.innerWidth/2,0,(rawmap[0].length*64)-window.innerWidth),Clamp(player.y-window.innerHeight/2,0,(rawmap.length*64)-window.innerHeight)]
      renderOffset=[-renderOffset[0],-renderOffset[1]]


      let playerrenderpos = [player.x+renderOffset[0],player.y+renderOffset[1]]


      draw(context, player,[playerrenderpos[0],playerrenderpos[1]]);

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