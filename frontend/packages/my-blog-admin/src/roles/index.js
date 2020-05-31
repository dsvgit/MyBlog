import React from "react";
import {
  List,
  Datagrid,
  TextField,
  Show,
  SimpleShowLayout,
  Create,
  SimpleForm,
  TextInput,
} from "react-admin";

export const RoleList = (props) => (
  <List {...props}>
    <Datagrid rowClick="show">
      <TextField source="name" />
    </Datagrid>
  </List>
);

export const RoleShow = (props) => (
  <Show {...props}>
    <SimpleShowLayout>
      <TextField source="name" />
    </SimpleShowLayout>
  </Show>
);

export const RoleCreate = (props) => (
  <Create {...props}>
    <SimpleForm>
      <TextInput source="name" />
    </SimpleForm>
  </Create>
);
