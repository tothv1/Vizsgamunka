import { Slime } from "./Slime";
import rightIdle from "../Assets/characters/slime.png"

const getImg = () =>{

    var temp = new Image();
    temp.src=rightIdle;
    return temp;
  }
  
class IntervalSpawner {
    ID=7;

    x = 0;
    y = 0;

    spawnRate = 3;
    nextSpawn = 0;
    maxSpawn = 3;

    obj = Slime;

    pool = []

    entityRef = []

    drawing = getImg();


    Spawn() {
        if (this.pool.length < this.maxSpawn && this.nextSpawn <= 0) {

            this.nextSpawn=this.spawnRate;
            let spawn = new this.obj();
            spawn.x = this.x;
            spawn.y = this.y;
            spawn.entityRef=this.entityRef

            spawn.init();

            this.pool.push(spawn)
            this.entityRef.entityList.push(spawn);
        }
    }

    Update(deltaTime, frameCount) {
        this.nextSpawn-=deltaTime;
        this.pool = this.pool.filter((xd) => !xd.dead);
        this.Spawn();
    }

}

export { IntervalSpawner }