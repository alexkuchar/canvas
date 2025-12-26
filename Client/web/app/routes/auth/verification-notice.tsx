import React from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "~/components/ui/card";
import { Button } from "~/components/ui/button";

const VerificationNotice = () => {
  return (
    <div className="h-screen w-screen flex flex-col items-center justify-center">
      <Card className="w-full max-w-md">
        <CardHeader>
          <CardTitle>Please verify your email</CardTitle>
          <CardDescription>
            We've sent you an email to verify your email address. Please check
            your email and click the link to verify your email address.
          </CardDescription>
        </CardHeader>
      </Card>
    </div>
  );
};

export default VerificationNotice;
