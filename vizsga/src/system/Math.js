export function Clamp(num, min, max) {
    if (num < min) return min;
    if (num > max) return max;
    if (min > max) return min;
    return num;
}

export function Normalise(value) {
    let Magnitude = Math.sqrt(Math.pow(value[0], 2) + Math.pow(value[1], 2))
    let normVector = [value[0] / Magnitude, value[1] / Magnitude]
    return normVector
}

export function GetDirection(point1,point2){
    return [point1[0]-point2[0],point1[1]-point2[1]];
}

export function LerpNum(num, target, rate) {
    let diff = target - num;
    let lerp = diff * rate;
    return num + lerp;
}

export function Lerp2D(num,target,rate){
    let diffx = num[0]-target[0];
    let diffy = num[1]-target[1];
    let lerpx = diffx*rate;
    let lerpy = diffy*rate;
    return [lerpx,lerpy];
}

export function Translate(num,translation){
    return [num[0]-translation[0],num[1]-translation[1]]
}

export function RadToDegrees(radian){
    return radian * (180 / Math.PI);
}

export function GetDirRad(direction) {
    return Math.atan2(direction[1],direction[0])-Math.PI/2;
}