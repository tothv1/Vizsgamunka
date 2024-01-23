import React, { useRef, useEffect } from 'react'

const Canvas = props => {

    const entities = props.entities;



    const canvasRef = useRef(null)
    let frameCount = 0


    const clrCanvas = (ctx) => {
        ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)


    }

    const draw = (ctx, object) => {

        console.log(object)

        let drawing = new Image();
        drawing.src = object.render;

        ctx.drawImage(drawing,object.x,object.y,object.w,object.h);
    }

    useEffect(() => {

        const canvas = canvasRef.current
        const context = canvas.getContext('2d')

        console.log(entities)

        let animationFrameId = 0

        //Our draw came here
        const render = () => {
            clrCanvas(context)

            const map = entities.wall;
            const slime = entities.slime;

            const rawmap = map.rawMap;

            for (let i = 0; i < rawmap.length; i++) {
                for (let j = 0; j < rawmap[i].length; j++) {
                  if (rawmap[i][j] === 1) {
                    const obj = {
                        render:map.render,
                        x:j*64,
                        y:i*64,
                        w:map.w,
                        h:map.h
                    }
                    draw(context,obj)
                  } else if(rawmap[i][j] === 2){
                    const obj = {
                        render:slime.render,
                        x:j*64,
                        y:i*64,
                        w:map.w,
                        h:map.h
                    }
                    draw(context,obj)
                  }
                }
              }


            

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