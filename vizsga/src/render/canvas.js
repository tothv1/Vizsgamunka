import React, { useRef, useEffect } from 'react'




const Canvas = props => {

    const entities = props.entities;



    const canvasRef = useRef(null)
    let frameCount = 0


    const clrCanvas = (ctx) => {
        ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)


    }

    const draw = (ctx, object) => {

        //console.log(object)

        let drawing = new Image();
        drawing.src = object.render;

        ctx.drawImage(drawing,object.x,object.y,object.w,object.h);
    }

    useEffect(() => {

        const canvas = canvasRef.current
        const context = canvas.getContext('2d')

        console.log(entities)

        let animationFrameId = 0

        let Runtime = 0;
        let lastUpdateTime = 0;

        //Our draw came here
        const render = () => {
            clrCanvas(context);
            Runtime=window.performance.now();

            let deltaTime = (Runtime-lastUpdateTime)/1000

            

            console.log(deltaTime)




            const map = entities.terrain;
            //const slime = entities.slime;

            const rawmap = map.rawMap;
            
            for (let i = 0; i < rawmap.length; i++) {
                for (let j = 0; j < rawmap[i].length; j++) {
                  if (rawmap[i][j] === 1) {

                    let obj = entities.terrain.update();
                    obj.x=j*64;
                    obj.y=i*64;

                    draw(context,obj)
                  } 
                  if (rawmap[i][j] === 2) {

                    let obj = entities.slime.update();
                    obj.x=j*64;
                    obj.y=i*64;

                    draw(context,obj)
                  } 
                }
              }


              draw(context,entities.player.update(deltaTime));


              lastUpdateTime=window.performance.now();
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