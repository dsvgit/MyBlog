import React from "react";
import {
  List,
  Datagrid,
  TextField,
  Show,
  SimpleShowLayout
} from "react-admin";

export const UserList = (props) => (
  <List {...props}>
    <Datagrid rowClick="show">
      <TextField source="email" />
    </Datagrid>
  </List>
);

export const UserShow = (props) => (
  <Show {...props}>
    <SimpleShowLayout>
      <TextField source="email" />
    </SimpleShowLayout>
  </Show>
);
