import React, { PureComponent } from "react";
import karakter from "../Assets/characters/hatternelkuli.png";
import wall from "../Assets/map/wall.png";


class Element {
  constructor(x, y, type) {
    this.x = x;
    this.y = y;
    this.type = type;
  }
}

class Box extends PureComponent {
  render() {
    const size = 100;
    const x = this.props.x - size / 2;
    const y = this.props.y - size / 2;

    var charX = x
    var windowX = this.props.windowWidth;
    var charY = y;
    var windowY = this.props.windowHeight;
    
    if (charX <= windowX && windowX >= charX && charY <= windowY && windowY >= charY) {
      console.log("inside");
    } else {
      console.log("outside");
    }
    console.log("Karakter: " + x, y);
    console.log("Pálya mérete:" + this.props.windowHeight, this.props.windowWidth);

      /*if(Xmin <= x <= Xmax && Ymin <= y <= Ymax) {
        console.log("inside");
      } else {
        console.log("outside");
      }*/

      //console.log(Xmin, Xmax, Ymin, Ymax);

    return (
    <img src={karakter} style={{ position: "absolute", width: size, height: size, left: x, top: y }} />
    );
  }
}

class Wall extends PureComponent {
  render() {
    const size = 50;
    const map = [];
    const mapProps = this.props.renderer.props.map;

    for (let i = 0; i < mapProps.length; i++) {
      for (let j = 0; j < mapProps[i].length; j++) {
        if (mapProps[i][j] === 1) {
          map.push(new Element(i * size, j * size, 1));
        }
      }
    }
    return (
      <div>
        {map.map((item, index) => (
          <img
            key={index}
            src={wall}
            style={{
              position: "absolute",
              width: size,
              height: size,
              left: item.y,
              top: item.x,
            }}
          />
        ))}
      </div>
    );
  }
}

export { Box, Wall };