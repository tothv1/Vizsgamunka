import React from 'react'
import { Link } from 'react-router-dom'
import './AuthPage.css'

const AuthPage = () => {
    return (
        <div className='container d-flex align-items-center justify-content-center min-vh-100 text-center auth-container'>
            <div>
                <button className='btn btn-primary'>Bejelentkezés</button><br />
                <Link to='/register'>Még nincs fiókod? Regisztrálj egyet!</Link>
                <br />
                <Link className='btn btn-primary' to='/game'>gameszkó teszteléshez gomb</Link>
            </div>
        </div>
    )
}

export default AuthPage