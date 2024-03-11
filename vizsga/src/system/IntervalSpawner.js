import { Slime } from "./Slime";


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

    Spawn() {
        if (this.pool.length < this.maxSpawn && this.nextSpawn <= 0) {

            let spawn = new this.obj();
            spawn.x = this.x;
            spawn.y = this.y;

            this.pool.push(spawn)
            this.entityRef.entityList.push(spawn);
        }
    }

    Update(deltaTime, frameCount) {
        this.pool = this.pool.filter((xd) => !xd.dead);
        this.Spawn();
    }

}

export { IntervalSpawner }