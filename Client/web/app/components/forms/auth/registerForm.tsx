import { useForm } from "@tanstack/react-form";
import * as z from "zod";
import axios from "axios";
import { Button } from "~/components/ui/button";
import {
  Field,
  FieldError,
  FieldGroup,
  FieldLabel,
} from "~/components/ui/field";
import { Input } from "~/components/ui/input";
import { toast } from "sonner";
import { AxiosError } from "axios";
import { useNavigate } from "react-router";
import { Spinner } from "~/components/ui/spinner";

const formSchema = z.object({
  fullName: z.string().min(1, { message: "Full name is required" }),
  email: z.email({ message: "Invalid email address" }),
  password: z
    .string()
    .min(8, { message: "Password must be at least 8 characters long" }),
});

const RegisterForm = () => {
  const navigate = useNavigate();

  const form = useForm({
    defaultValues: {
      fullName: "",
      email: "",
      password: "",
    },
    validators: {
      onChange: formSchema,
      onSubmit: formSchema,
    },
    onSubmit: async (values) => {
      // try {
      //   await axios.post("http://localhost:5163/api/Auth/register", {
      //     firstName: values.value.fullName.split(" ")[0],
      //     lastName: values.value.fullName.split(" ")[1],
      //     email: values.value.email,
      //     password: values.value.password,
      //   });
      //   toast.success("Registration successful");
      //   form.reset();
      //   navigate("/verify-email");
      // } catch (error) {
      //   const err = error as AxiosError;
      //   toast.error(
      //     (err.response?.data as { error: { message: string } }).error.message
      //   );
      //   throw error;
      // }
      await axios
        .post("http://localhost:5163/api/Auth/register", {
          firstName: values.value.fullName.split(" ")[0],
          lastName: values.value.fullName.split(" ")[1],
          email: values.value.email,
          password: values.value.password,
        })
        .then((response) => {
          toast.success("Registration successful");
          form.reset();
          navigate("/verify-email");
        })
        .catch((error) => {
          const err = error as AxiosError;
          toast.error(
            (err.response?.data as { error: { message: string } }).error.message
          );
          throw error;
        });
    },
  });
  return (
    <form
      id="register-form"
      onSubmit={(e) => {
        e.preventDefault();
        form.handleSubmit();
      }}
    >
      <FieldGroup>
        <form.Field
          name="fullName"
          children={(field) => {
            const isInvalid =
              field.state.meta.isTouched && !field.state.meta.isValid;

            return (
              <Field data-invalid={isInvalid}>
                <FieldLabel htmlFor={field.name}>Full Name</FieldLabel>
                <Input
                  id={field.name}
                  name={field.name}
                  value={field.state.value}
                  onBlur={field.handleBlur}
                  onChange={(e) => field.handleChange(e.target.value)}
                />
                {isInvalid && <FieldError errors={field.state.meta.errors} />}
              </Field>
            );
          }}
        />
        <form.Field
          name="email"
          children={(field) => {
            const isInvalid =
              field.state.meta.isTouched && !field.state.meta.isValid;

            return (
              <Field data-invalid={isInvalid}>
                <FieldLabel htmlFor={field.name}>Email</FieldLabel>
                <Input
                  type="email"
                  id={field.name}
                  name={field.name}
                  value={field.state.value}
                  onBlur={field.handleBlur}
                  onChange={(e) => field.handleChange(e.target.value)}
                />
                {isInvalid && <FieldError errors={field.state.meta.errors} />}
              </Field>
            );
          }}
        />
        <form.Field
          name="password"
          children={(field) => {
            const isInvalid =
              field.state.meta.isTouched && !field.state.meta.isValid;

            return (
              <Field data-invalid={isInvalid}>
                <FieldLabel htmlFor={field.name}>Password</FieldLabel>
                <Input
                  type="password"
                  id={field.name}
                  name={field.name}
                  value={field.state.value}
                  onBlur={field.handleBlur}
                  onChange={(e) => field.handleChange(e.target.value)}
                />
                {isInvalid && <FieldError errors={field.state.meta.errors} />}
              </Field>
            );
          }}
        />

        <form.Subscribe
          selector={(state) => ({
            isValid: state.isValid,
            isSubmitting: state.isSubmitting,
          })}
        >
          {(state) => (
            <Button
              type="submit"
              disabled={!state.isValid || state.isSubmitting}
            >
              {state.isSubmitting ? <Spinner /> : "Continue"}
            </Button>
          )}
        </form.Subscribe>
      </FieldGroup>
    </form>
  );
};

export default RegisterForm;
