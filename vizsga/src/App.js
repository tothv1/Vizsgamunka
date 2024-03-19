import React, { PureComponent, useState } from "react";
//import { GameEngine } from "react-game-engine";
//import Canvas from "./render/canvas";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Menu from "./menu";
import AuthPage from "./pages/AuthPage";
import Login from "./pages/Login";
import Register from "./pages/Register";
import { jwtDecode } from "jwt-decode";
import AdminPage from "./pages/AdminPage";
import ConfirmPage from "./pages/ConfirmPage";

/*import wall from "./Assets/map/wall.png";
import karakter from "./Assets/characters/hatternelkuli.png";
import slime from "./Assets/characters/slime.png";

import { Update } from "./system/Player";
import { UpdateT } from "./system/StoneWall";
import { UpdateSl } from "./system/Slime";
import { rawMaps } from "./Assets/map/maps";*/


export default function App() {

  var userRole = "Visitor";

  const [token, setToken] = useState(localStorage.getItem("token") ? localStorage.getItem("token") : null);
  const [isLoggedIn, setIsLoggedIn] = useState(token ? true : false);
  const [tokenData, setTokenData] = useState(token ? jwtDecode(token) : null);
  const [role, setRole] = useState(tokenData != null ? tokenData.role : "Visitor");

  console.log(tokenData);
  console.log(isLoggedIn);
  console.log(role);

  return (
      <Router>
        <Routes>
          <Route path="/" element={isLoggedIn ? <Menu isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} token={token} role={role} setRole={setRole} tokenData={tokenData} setTokenData={setTokenData} /> : <AuthPage />} />
          <Route path="/menu" element={<Menu isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} token={token} role={role} setRole={setRole} tokenData={tokenData} setTokenData={setTokenData} />} />
          <Route path="/login" element={<Login setIsLoggedIn={setIsLoggedIn} isLoggedIn={isLoggedIn} token={token} setToken={setToken} setRole={setRole} tokenData={tokenData} setTokenData={setTokenData} />} />
          <Route path="/admin" element={<AdminPage />} />
          <Route path="/register" element={<Register />} />
          <Route path="/confirm/:confirmKey" element={<ConfirmPage />} />
        </Routes>
      </Router>

  );
}
