import { GameLoop } from "react-game-engine";

const MoveBox = (entities, { input, time }) => {
  //-- I'm choosing to update the game state (entities) directly for the sake of brevity and simplicity.
  //-- There's nothing stopping you from treating the game state as immutable and returning a copy..
  //-- Example: return { ...entities, t.id: { UPDATED COMPONENTS }};
  //-- That said, it's probably worth considering performance implications in either case.

  const { payload } = input.find(x => x.name === "onKeyDown") || {};

  if (payload) {
    const box1 = entities["box1"];
    const speed = 1000;
    const deltaTime = time.delta / 1000;

    switch (payload.key) {
      case "w":
        box1.y -= speed * deltaTime;
        break;
      case "s":
        box1.y += speed * deltaTime;
        break;
      case "a":
        box1.x -= speed * deltaTime;
        break;
      case "d":
        box1.x += speed * deltaTime;
        break;

      default:
        break;
    }
  }

  return entities;
};

export { MoveBox };