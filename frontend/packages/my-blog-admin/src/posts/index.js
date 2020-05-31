import React from "react";
import {
  List,
  Datagrid,
  TextField,
  Show,
  SimpleShowLayout,
  RichTextField
} from "react-admin";

import './styles.css';

export const PostList = (props) => (
  <List {...props}>
    <Datagrid rowClick="show">
      <TextField source="title" />
      <RichTextField source="text" />
    </Datagrid>
  </List>
);

export const PostShow = (props) => (
  <Show {...props}>
    <SimpleShowLayout>
      <TextField source="title" />
      <RichTextField source="text" />
    </SimpleShowLayout>
  </Show>
);
