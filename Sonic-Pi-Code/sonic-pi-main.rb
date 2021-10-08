# Welcome to Sonic Pi
use_real_time

use_osc "localhost", 7000 # sender only

# init so state getting messed up doesnt stop you from starting over
set :reverbVal, 0
set :bitCrusherVal, 16

#LIB_ROOT = <SONIC PI FOLDER>
TAMBORINE = load_samples(LIB_ROOT + "tamborine/")

N64 = 1.0/64.0
N32 = 1.0/32.0
N16 = 1.0/16.0
N8 = 1.0/8
N4 = 1.0/4
N2 = 1.0/2

# one over
def oo(x)
  return 1.0 / x
end


def play_samples_timed(samples, times)
  timesFinal = times.ring
  samples.length.times do |i|
    sample samples[i]
    sleep timesFinal[i]
  end
  
end

def drum_sam (sampleX, sleepX)
  sample sampleX, amp: 0.5
  sleep sleepX
end

def xyl_play(note, sleepX)
  play note
  sleep sleepX
end

def bd_osc
  osc "/trees/bounce"
end

def choose_n(arr, n)
  out = []
  n.times do
    out.append(arr.choose)
  end
  
  return out
end



live_loop :metronome do
  sleep 1
end

live_loop :xylo, sync: :metronome do
  use_synth :kalimba
  
  4.times do
    myChord = (chord :a4, :major, num_octaves: 3)
    notes = choose_n(myChord, 3)
    play_pattern_timed notes, 0.5, amp: 4
    sleep 0.5
  end
  
  sleep 1 if one_in(2)
end






live_loop :walker, sync: :metronome do
  use_synth :mod_fm
  
  with_fx :reverb, room: 0.8 do
    ##| with_fx :ring_mod, mix: 0.5 do
    last_note = note (get[:walker_last_note] || :a4)
    chords = [:a4, :ab4, :gb3, :fb3].ring
    seasons = ["summer", "fall", "winter", "spring"]
    chord_to_season = Hash[chords.to_a.zip(seasons)]
    
    final_note = chords.tick
    play final_note, amp: 0.1, attack: 0.1, sustain: 0, release: 1.5, decay: 0.5
    
    season = chord_to_season[final_note]
    osc "/season", season
    sleep 2
    
    set :walker_last_note, final_note
    ##| end
  end
end

##| live_loop :ambi do

##| end

live_loop :midi do
  def chipbass_to_osc_address(note)
    return "/light/above"
    ##| prefix = "/light/above/"
    ##| x = note > 40 ? "high" : "low"
    ##| return prefix + x
  end
  
  note, vel = sync "/midi:lpk*/note_on"
  synth :kalimba, note: note, amp: vel / 127.0
  
  osc chipbass_to_osc_address(note), note
end


##| b = buffer(:looperbuf, 4)
##| live_loop :looper do
##|   with_fx :record, buffer: b do
##|     sample b #play back what was recorded
##|     live_audio :qsynth, amp: 1.27 # receive audio to be recorded
##|     sleep 4
##|   end
##| end

#####
## Regular loops that use fx wrapper
##

##| live_loop :mod_note do
##|   fx {
##|     synth :mod_sine, note: 60
##|     sleep 2
##|   }
##| end



##| live_loop :cymbal_crusher do
##|   ##| sync :bitCrusherVal # set & cue rely on same mechanism, so we can sync on this
##|   fx {
##|     sample :drum_cymbal_closed
##|     sleep n8
##|   }
##| end


#####
## Control fns - defining fx wrapper, and modifying the global state
##

##| # live loop so you can change the functions. performance hit maybe?
##| live_loop :fx_loop do
##|   # wrapper function
##|   def fx
##|     with_fx :reverb, room: get[:reverbVal] do
##|       with_fx :bitcrusher, bits: [get[:bitCrusherVal], 1].max do yield end
##|     end
##|   end

##|   sleep 1
##| end

##| live_loop :increase_fx do
##|   val = 0
##|   loopCount = 64
##|   loopCount.times do |i|
##|     normI = i.to_f / loopCount
##|     val = (0.2 + normI ** 3)
##|     set :reverbVal, val
##|     set :bitCrusherVal, 16 - (1 + val * 16)
##|     sleep n16
##|   end
##| end

##| ## ---- Unrelated


##| live_loop :wait do
##|   fx {
##|     puts "x"
##|     sync "/osc*/woot"
##|     puts "y"
##|     sample :drum_cymbal_closed
##|     sleep 1
##|   }
##| end