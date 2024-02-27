import React from 'react'
import { Link } from 'react-router-dom'

const AuthPage = () => {
    return (
        <div className='container d-flex  
        align-items-center  
        justify-content-center  
        min-vh-100 text-center'>
            <div>
                <button className='btn btn-primary'>Regisztráció</button><br />
                <Link to='/login'>Már van felhasználója? Bejelentkezés</Link>
                <br />
                <Link className='btn btn-primary' to='/game'>gameszkó teszteléshez gomb</Link>
            </div>
        </div>
    )
}

export default AuthPage