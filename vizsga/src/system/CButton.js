import { useNavigate } from 'react-router-dom';

class Button{
    
    xOffset = 100;
    yOffset = 100;

    width=300;
    height=50;

    xOffset = 30;
    yOffset = 30;

    role="";
}

class QuitButton extends Button{
    role="quit";
}

export {QuitButton}