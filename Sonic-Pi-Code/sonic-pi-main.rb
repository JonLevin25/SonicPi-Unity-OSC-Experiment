# Welcome to Sonic Pi
use_real_time

use_osc "localhost", 4561 # sender only
def fx
  val = get[:x]
  with_fx :bitcrusher, mix: val do
    yield
  end
end

live_loop :test_osc do
  fx {
    
  }
end

live_loop :test_osc do
  v = 0
    4.times do
  fx {
      sample :drum_cymbal_closed
      set :x, v
      osc "/sonic/bd", 1.25
      sleep 1
      puts v
  }
  v += 0.25
    end
end

live_loop :wait do
  fx {
    puts "x"
  sync "/osc*/woot"
  puts "y"
  sample :drum_cymbal_closed
  }
end