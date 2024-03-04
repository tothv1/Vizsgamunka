import React, { useState } from "react";
//import Canvas from "./render/canvas";
import { useHistory, useNavigate } from 'react-router-dom';
import { Link } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import AuthPage from "./pages/AuthPage";

const Menu = ({ isLoggedIn, setIsLoggedIn, token, role, tokenData }) => {
  //bootstrapet, react-router-dom -ot majd installálni
  const navigate = useNavigate();
  
  const [showAchievements, setShowAchievements] = useState(false);
  const [showStats, setShowStats] = useState(false);
  const [showSettings, setShowSettings] = useState(false);

  //az achievement jobb oldalon megjelenik, ha rákattintasz mégegyszer a gombra akkor eltűnik, vagy kéne egy kilépés gomb
  const handleAchievement = () => {
    setShowAchievements(!showAchievements);
  }

  //a stats jobb oldalon megjelenik, ha rákattintasz mégegyszer a gombra akkor eltűnik, vagy kéne egy kilépés gomb
  const handleStats = () => {
    setShowStats(!showStats);
  }

  //a settings jobb oldalon megjelenik, ha rákattintasz mégegyszer a gombra akkor eltűnik, vagy kéne egy kilépés gomb
  const handleSettings = () => {
    setShowSettings(!showSettings);
  }

  //style={{ position: 'fixed', top: '50%', right: '10px', transform: 'translateY(-50%)', backgroundColor: 'white', padding: '20px', boxShadow: '0 0 10px rgba(0, 0, 0, 0.3)', zIndex: 999 }}
 

  return (

    (role !== "Visitor" && isLoggedIn) ?
    (<div className="container">
      <div className="row">
        <div className="col border border-primary">
        <h1>Játék Menü</h1>
        <img src="https://via.placeholder.com/300x300" alt="LOGÓ" title="LOGÓ" />
          <div className="mb-3 p-2">
            {/* Gombok */}
            <button className="btn btn-primary mb-2" onClick={ async () => {
                var response = await axios.put(`https://localhost:7096/Auth/logout?token=${token}`, {},{
                  headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                  }
                });
                console.log(response.data);
                if(response.data.status == 200) {
                  setIsLoggedIn(false);
                  localStorage.removeItem('token');
                  navigate('/');
                  console.log("Sikeres kijelentkezés");
                }
            }}> 
              {isLoggedIn ? `Kijelentkezés (${tokenData.username})` : "Bejelentkezés"}
            </button>
            <br />
            <Link to="/game" className="btn btn-primary mb-2">Play</Link>
            <br />
            <button className="btn btn-primary mb-2" onClick={handleAchievement}>Achievement</button>
            <br />
            <button className="btn btn-primary mb-2" onClick={handleStats}>Stats</button>
            <br />
            <button className="btn btn-primary mb-2" onClick={handleSettings}>Settings</button>
          </div>
        </div>
        <div className="col border border-primary">
          {/* Majd ide jönne a logó/játék neve*/}
          {/* <img scr=""><img> */}
          <div className="mb-6">
            {/* karakterek/artok */}
            {/* Achievementsek megjelenítve */}
            {showAchievements && (
              <div>
                <h2>Achievements:</h2>
                <ul>
                  <li>Achievement 1</li>
                  <li>Achievement 2</li>
                  <li>Achievement 3</li>
                </ul>
              </div>
            )}

            {/* Stat megjelenítése */}
            {showStats && (
              <div>
                <h2>Statisztikák:</h2>
                <p>Stat 1</p>
                <p>Stat 2</p>
                <p>Stat 3</p>
                <p>Stat 4</p>
                {/* Statok felsorolása */}
              </div>
            )}

            {/* Beállítások megjelenítése */}
            {showSettings && (
              <div>
                <h2>Beállítások:</h2>
                {/* Beállítások megjelenítése */}
              </div>
            )}

          </div>
        </div>
      </div>
    </div>) : 
    (<div>
      <AuthPage />
    </div>)
  );
};

export default Menu;
