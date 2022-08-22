import { Form, Field } from "react-final-form";
import * as yup from "yup";

import { validateFormValues } from "../../framework/validation";
import request from "../../request";

const validate = validateFormValues(
  yup.object({
    email: yup.string().email(),
    password: yup.string().required(),
  })
);

export default function Login() {
  async function login(values) {
    const { email, password } = values;
    try {
      const response = await request.post("api/account/login", {
        email,
        password,
      });
      console.log("response", response);
    } catch (error) {
      console.log("error", error);
    }
  }

  return (
    <div>
      <div className="w-50 m-auto">
        <h2>Вход на сайт</h2>

        <Form
          onSubmit={login}
          initialValues={{
            email: "",
            password: "",
          }}
          validate={validate}
          render={({ handleSubmit }) => {
            return (
              <form onSubmit={handleSubmit}>
                <Field name="email">
                  {({ input, meta }) => (
                    <div className="form-group">
                      <label htmlFor="email">Введите Email</label>
                      <input
                        {...input}
                        id="email"
                        type="text"
                        className="form-control"
                      />
                      {meta.touched && meta.error && (
                        <span className="invalid-feedback">{meta.error}</span>
                      )}
                    </div>
                  )}
                </Field>

                <Field name="password">
                  {({ input, meta }) => (
                    <div className="form-group">
                      <label htmlFor="password">Введите пароль</label>
                      <input
                        {...input}
                        id="password"
                        type="password"
                        className="form-control"
                      />
                      {meta.touched && meta.error && (
                        <span className="invalid-feedback">{meta.error}</span>
                      )}
                    </div>
                  )}
                </Field>

                <div className="form-group d-flex justify-content-between align-items-center">
                  <input
                    type="submit"
                    value="Войти"
                    className="btn btn-primary"
                  />
                  <a>Зерегистрироваться</a>
                </div>
              </form>
            );
          }}
        />
      </div>
    </div>
  );
}
