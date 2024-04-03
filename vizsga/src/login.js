import React from 'react';
import '../public/index.css';

const Login = () => {
    return (
        <div className='container w-50 border border-dark mt-5 loginhatter'>
            <form onSubmit={ () => { 
                

            } } className='form-control'>
                <h1>Login</h1>
                <div className='form-group m-2'>
                    <label htmlFor='username'>Username</label>
                    <input type='text' className='form-control' id='username' />
                </div>
                <div className='form-group m-2'>
                    <label htmlFor='password'>Password</label>
                    <input type='password' className='form-control' id='password' />
                </div>
                <button type='submit' className='btn btn-primary'>Bejelentkezés</button>
            </form>
        </div>
    )
}

export default Login