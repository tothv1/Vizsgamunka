import React, { PureComponent, useState } from "react";
import Canvas from "./render/canvas";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Menu from "./menu";
import AuthPage from "./pages/AuthPage";
import Login from "./pages/login";
import Register from "./pages/register";
import { jwtDecode } from "jwt-decode";
import AdminPage from "./pages/AdminPage";
import ConfirmPage from "./pages/ConfirmPage";
import "./pages/index.css";

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
          <Route path="/game" element={isLoggedIn ? <Canvas tokendata={tokenData} style= {{width:1200, height:800, backgroundColor : "lightblue"}}/> : <Login setIsLoggedIn={setIsLoggedIn} isLoggedIn={isLoggedIn} token={token} setToken={setToken} setRole={setRole} tokenData={tokenData} setTokenData={setTokenData} />} />
          <Route path="/register" element={<Register />} />
          <Route path="/confirm/:confirmKey" element={<ConfirmPage />} />

        </Routes>
      </Router>

  );
}
