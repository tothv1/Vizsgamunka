import React, { useState } from "react";
import Canvas from "./render/canvas";
import { useHistory } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

const Menu = () => {
  //bootstrapet, react-router-dom -ot majd installálni

  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUserName] = useState("");
  const [showAchievements, setShowAchievements] = useState(false);
  const [showStats, setShowStats] = useState(false);
  const [showSettings, setShowSettings] = useState(false);

  const handleLogin = () => {
    // Ha rákattint akkor bejelentkezik és a login helyén a player neve lesz, mellette a kijelentkezés
    setIsLoggedIn(true);
    setUserName("UserName");
  };

  const handleLogout = () => {
    // Kijelentkezés
    setIsLoggedIn(false);
    setUserName("");
  };

  //login/logout button
  const handleLoginButtonClick = () => {
    if (isLoggedIn) {
      handleLogout();
    } else {
      handleLogin();
    }
  };


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

  return (
    <div className="container">
      <h1>Játék Menü</h1>
      {/* Majd ide jönne a logó/játék neve*/}
      {/* <img scr=""><img> */}
      <div className="mb-6">
        {/* Gombok */}

        <button onClick={handleLoginButtonClick}>
          {isLoggedIn ? `Kijelentkezés (${username})` : "Bejelentkezés"}
        </button>
        <br/>
        <button>Play</button>
        <br/>
        <button onClick={handleAchievement}>Achievement</button>
        <br/>
        <button onClick={handleStats}>Stats</button>
        <br/>
        <button onClick={handleSettings}>Settings</button>

        
      </div>
      <div className="mb-6">

        {/* karakterek/artok */}

        {/* Achievementsek megjelenítve */}
        {showAchievements && (
        <div style={{ position: 'fixed', top: '50%', right: '10px', transform: 'translateY(-50%)', backgroundColor: 'white', padding: '20px', boxShadow: '0 0 10px rgba(0, 0, 0, 0.3)', zIndex: 999 }}>
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
        <div style={{ position: 'fixed', top: '50%', right: '10px', transform: 'translateY(-50%)', backgroundColor: 'white', padding: '20px', boxShadow: '0 0 10px rgba(0, 0, 0, 0.3)', zIndex: 999 }}>
          <h2>Statisztikák:</h2>
          {/* Statok felsorolása */}
        </div>
      )}

      {/* Beállítások megjelenítése */}
      {showSettings && (
        <div style={{ position: 'fixed', top: '50%', right: '10px', transform: 'translateY(-50%)', backgroundColor: 'white', padding: '20px', boxShadow: '0 0 10px rgba(0, 0, 0, 0.3)', zIndex: 999 }}>
          <h2>Beállítások:</h2>
          {/* Beállítások megjelenítése */}
        </div>
      )}

      </div>
    </div>
  );
};

export default Menu;
