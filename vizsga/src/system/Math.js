export function Clamp(num, min, max) {
    if (num < min) return min;
    if (num > max) return max;
    return num;
}



export function Normalise(value) {



    let Magnitude = Math.sqrt(Math.pow(value[0],2) + Math.pow(value[1],2))

    let normVector = [value[0]/Magnitude,value[1]/Magnitude]

    return normVector
}


//valszeg nem máködik ahogy kéne xd
export function LerpNum(num, target, rate) {
    let diff = Math.abs(target - num);
    let lerp = diff * rate;
    if (target > num) return num + lerp
    return num - lerp;
}
