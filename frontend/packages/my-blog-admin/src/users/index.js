import React from "react";
import {
  List,
  Datagrid,
  TextField,
  Show,
  SimpleShowLayout,
  Create,
  Edit,
  TextInput,
  SimpleForm,
  ReferenceInput,
  SelectInput,
  ReferenceArrayField,
  SingleFieldList,
  ChipField,
  TopToolbar,
  EditButton,
  ReferenceArrayInput,
  AutocompleteArrayInput
} from "react-admin";

export const UserList = (props) => (
  <List {...props}>
    <Datagrid rowClick="show">
      <TextField source="email" />
      <EditButton />
    </Datagrid>
  </List>
);

const UserShowActions = ({ basePath, data, resource }) => (
  <TopToolbar>
    <EditButton basePath={basePath} record={data} />
  </TopToolbar>
);

export const UserShow = (props) => (
  <Show actions={<UserShowActions />} {...props}>
    <SimpleShowLayout>
      <TextField source="email" />
      <ReferenceArrayField label="Roles" source="roleIds" reference="roles">
        <SingleFieldList>
          <ChipField source="name" />
        </SingleFieldList>
      </ReferenceArrayField>
    </SimpleShowLayout>
  </Show>
);

export const UserCreate = (props) => (
  <Create {...props}>
    <SimpleForm>
      <TextInput source="email" />
      <TextInput source="password" />
      <ReferenceArrayInput label="Roles" source="roleIds" reference="roles">
        <AutocompleteArrayInput />
      </ReferenceArrayInput>
    </SimpleForm>
  </Create>
);

export const UserEdit = (props) => (
  <Edit {...props}>
    <SimpleForm>
      <TextField source="email" />
      <ReferenceArrayInput label="Roles" source="roleIds" reference="roles">
        <AutocompleteArrayInput />
      </ReferenceArrayInput>
    </SimpleForm>
  </Edit>
);
