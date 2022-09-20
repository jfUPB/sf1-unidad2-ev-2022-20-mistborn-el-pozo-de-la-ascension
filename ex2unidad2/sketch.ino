String btnState(uint8_t btnState){
  if(btnState == HIGH){
    return "OFF";
  }
  else return "ON";
}

void task()
{
  enum class TaskStates
  {
    INIT,
    WAIT_COMMANDS
  };
  static TaskStates taskState = TaskStates::INIT;
  constexpr uint8_t button1Pin = 13;
  constexpr uint8_t button2Pin = 12;
  constexpr uint8_t button3Pin = 11;
  constexpr uint8_t led1 = 7;
  constexpr uint8_t led2 = 6;
  constexpr uint8_t led3 = 5;
  static uint32_t lasTime;
  static bool ledStatus = false;

  switch (taskState)
  {
  case TaskStates::INIT:
  {
    Serial.begin(9600);
    pinMode(button1Pin, INPUT_PULLUP);
    pinMode(button2Pin, INPUT_PULLUP);
    pinMode(button3Pin, INPUT_PULLUP);
    pinMode(led1, OUTPUT);
    digitalWrite(led1, LOW);
    pinMode(led2, OUTPUT);
    digitalWrite(led2, LOW);
    pinMode(led3, OUTPUT);
    digitalWrite(led3, LOW);

    taskState = TaskStates::WAIT_COMMANDS;

    
    break;
  }
  case TaskStates::WAIT_COMMANDS:
  {
    
    if (Serial.available() > 0)
    {
      String command = Serial.readStringUntil('\n');
      
      if (command == "led1ON")
      {
        digitalWrite(led1, HIGH);
      }
      else if (command == "led1OFF")
      {
        digitalWrite(led1, LOW);
      }
      else if (command == "led2ON")
      {
        digitalWrite(led2, HIGH);
      }
      else if (command == "led2OFF")
      {
        digitalWrite(led2, LOW);
      }
      else if (command == "led3ON")
      {
        digitalWrite(led3, HIGH);
      }
      else if (command == "led3OFF")
      {
        digitalWrite(led3, LOW);
      }
      
      if(command == "readBUTTONS")
      {
        /*
        Serial.print(btnState(digitalRead(button1Pin)).c_str());
        Serial.print('\n');
        if(command == "readNext")
        {
          Serial.print(btnState(digitalRead(button2Pin)).c_str());
          Serial.print('\n');
        }
        else if (command == "readNext2")
        {
          Serial.print(btnState(digitalRead(button3Pin)).c_str());
          Serial.print('\n');
        }
        */
        Serial.print(btnState(digitalRead(button1Pin)).c_str());
        Serial.print(btnState(digitalRead(button2Pin)).c_str()); 
        Serial.print(btnState(digitalRead(button3Pin)).c_str());
        Serial.print('\n');
      }
    }
    break;
  }
  default:
  {
    break;
  }
  }
}

void setup()
{
  task();
}

void loop()
{
  task();
}