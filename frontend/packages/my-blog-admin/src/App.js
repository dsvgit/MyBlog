import React from "react";
import { Admin, Resource } from "react-admin";
import makeDataProvider from "./dataProvider";
import {UserList, UserShow} from "./users";

const dataProvider = makeDataProvider("https://localhost:5001/api");
function App() {
  return (
    <Admin dataProvider={dataProvider}>
      <Resource name="users" list={UserList} show={UserShow} />
    </Admin>
  );
}

export default App;
