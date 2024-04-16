import React from 'react'
import { Link } from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'
import "./index.css";


const AuthPage = () => {
    return (
        <div className='homalyoshatter'>
        <div className='container-fluid d-flex align-items-center justify-content-md-center text-center vh-100'>
            <div>
                <Link to={'/login'} className='btn btn-primary'>Bejelentkezés</Link><br /> <br/>
                <Link to='/register'>Még nincs fiókod?</Link>
            </div>
        </div>
        </div>
    )
}

export default AuthPage