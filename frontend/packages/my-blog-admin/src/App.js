import React from "react";
import { Admin, Resource } from "react-admin";
import { createMuiTheme } from '@material-ui/core/styles';
import PostsIcon from '@material-ui/icons/Book';
import UsersIcon from '@material-ui/icons/Group';
import RolesIcon from '@material-ui/icons/SupervisedUserCircle';

import makeDataProvider from "./dataProvider";
import { UserList, UserShow, UserCreate, UserEdit } from "./users";
import { RoleList, RoleShow, RoleCreate } from "./roles";
import { PostList, PostShow } from "./posts";

const dataProvider = makeDataProvider("https://localhost:5001/api");

const theme = createMuiTheme({
  palette: {
    primary: { main: 'rgba(42,99,222,0.97)' },
    secondary: { main: 'rgba(42,99,222,0.97)' }
  },
  typography: {
    fontFamily: [
      'Muli',
      '-apple-system',
      'BlinkMacSystemFont',
      '"Segoe UI"',
      'Roboto',
      '"Helvetica Neue"',
      'Arial',
      'sans-serif',
      '"Apple Color Emoji"',
      '"Segoe UI Emoji"',
      '"Segoe UI Symbol"',
    ].join(','),
  },
  sidebar: {
    width: 300
  }
});

function App() {
  return (
    <Admin theme={theme} dataProvider={dataProvider}>
      <Resource
        name="posts"
        list={PostList}
        show={PostShow}
        icon={PostsIcon}
      />
      <Resource
        name="users"
        list={UserList}
        show={UserShow}
        create={UserCreate}
        edit={UserEdit}
        icon={UsersIcon}
      />
      <Resource
        name="roles"
        list={RoleList}
        show={RoleShow}
        create={RoleCreate}
        icon={RolesIcon}
      />
    </Admin>
  );
}

export default App;
