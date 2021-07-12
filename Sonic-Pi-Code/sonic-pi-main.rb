# Welcome to Sonic Pi
use_osc "localhost", 4561
live_loop :test_osc do
  sample :bd_haus
  osc "/sonic/bd", 1.25
  sleep 1
end