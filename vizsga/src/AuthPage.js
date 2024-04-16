import React from 'react'
import { Link } from 'react-router-dom'
import "./pages/index.css";

const AuthPage = () => {
    return (
        <div className='homalyoshatter'> 

        
        <div className='container '>
            <div>
                <button className='btn btn-primary'>Regisztráció</button><br />
                <Link to='/login'>Már van felhasználója? Bejelentkezés</Link>
                <br />
                <Link className='btn btn-primary' to='/game' >gameszkó teszteléshez gomb</Link>
            </div>
        </div>
        </div>
    )
}

export default AuthPage