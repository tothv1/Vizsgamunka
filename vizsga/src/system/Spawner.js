import { Slime } from "./Slime";
import rightIdle from "../Assets/characters/slime.png"
import { CreateRandomDirection, getRandomRange } from "./Math";

const getImg = () =>{

    var temp = new Image();
    temp.src=rightIdle;
    return temp;
  }
  
class Spawner {
    ID=7;

    x = 0;
    y = 0;

    enemyScale = 1;
    enemyScaleMult = 1;

    spawnDistanceAvg = 700;

    baseSpawnInterval = 0.05;
    nextSpawn = 0;
    aliveTime = 0;

    obj = Slime;

    pool = []
    spawnPool = 0;

    entityRef = []
    playerRef = Object

    drawing = getImg();


    Spawn() {
        if (this.pool.length*this.baseSpawnInterval<=this.spawnPool) {

            this.spawnPool-=this.pool.length*this.baseSpawnInterval;

            let spawn = new this.obj();

            let instanceSpawnDistance = getRandomRange(this.spawnDistanceAvg* 0.9,this.spawnDistanceAvg* 1.1);

            let dir = CreateRandomDirection();

            spawn.x = this.playerRef.x+(dir[0]*instanceSpawnDistance);
            spawn.y = this.playerRef.y+(dir[1]*instanceSpawnDistance);

            spawn.speed*=(1+(Math.sqrt(this.enemyScale*this.enemyScaleMult)));
            spawn.maxHealth *=(1+ this.enemyScale*this.enemyScaleMult);
            spawn.Damage = Math.round(spawn.Damage*(1+this.enemyScale*this.enemyScaleMult));

            spawn.health=spawn.maxHealth;

            spawn.entityRef=this.entityRef

            spawn.init();

            this.pool.push(spawn)
            this.entityRef.entityList.push(spawn);
        }
    }

    Update(deltaTime, frameCount) {
        this.spawnPool+=deltaTime;
        this.pool = this.pool.filter((xd) => !xd.dead);
        this.Spawn();
    }

}

export { Spawner }