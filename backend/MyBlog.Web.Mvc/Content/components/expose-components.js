import "regenerator-runtime/runtime.js";

import React from "react";
import ReactDOM from "react-dom";
import ReactDOMServer from "react-dom/server";

import Home from "./Home.jsx";
import Contacts from "./Contacts.jsx";
import PostsIndex from "./Posts/Index.jsx";
import AccountLogin from "./Account/Login.jsx";

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;

global.Components = { Home, AccountLogin, Contacts, PostsIndex };
