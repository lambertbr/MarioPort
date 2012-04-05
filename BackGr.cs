using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Uses Buffers, Vga256(not needed), Palettes

namespace MarioFiles
{
   class BackGr
   {
      //constants
      public const int Left = 0;
      public const int Right = 1;
      public const int Shift = 16;
      private const int Speed = 3;
      private const int BrickSpeed = 2;
      private const int Max = (Buffers.MaxWorldSize / Speed) * Buffers.W;
      private const int Height = 26;
      private const int CloudSpeed = 4;
      private const int MaxClouds = 7;
      private const int MinCloudSize = 30;
      private const int MaxCloudSize = 70;
      private const int CloudHeight = 19;

      //Array Sizes
      private const int COLORMAP_MAX = Buffers.NV * Buffers.H - 1;

      //variables
      public byte BackGround;
      private byte[] BackGrMap = new byte[Max];
      private ushort[] ColorMap = new ushort[COLORMAP_MAX];
      private int[,] CloudMap = new int[MaxClouds - 1, 2];
      private byte Clouds;

      //Methods
      public void InitBackGr(byte NewBackGr, byte bClouds)
      {
         int i, j, h;
         //  X, Y, Z: Real;
         //  F: Text;
         BackGround = NewBackGr;
         if(BackGround == 1 || BackGround == 2)
            move(@BOGEN^, BackGrMap, BackGrMap.Length);
         else if(BackGround == 3)
            move(@MOUNT^, BackGrMap, BackGrMap.Length);
         else if(BackGround == 9)
            move(@BOGEN7^, BackGrMap, BackGrMap.Length);
         else if(BackGround == 10)
            move(@BOGEN26^, BackGrMap, BackGrMap.Length);
         if(BackGround == 1 || BackGround == 9 || BackGround == 10)
            for( i = 0; i <= Max; i++)
              BackGrMap[i] = Convert.ToByte(Height - BackGrMap [i] + 1);
         Clouds = bClouds;
         if (Clouds != 0)
            InitClouds();
      }

      public void DrawBackGr(bool FirstTime)
      {
         int i;
         if(BackGround >= 1 && BackGround <= 3)
            PutBackGr(BackGrMap, FirstTime);
         if(BackGround >= 9 && BackGround <= 11)
            PutBackGr(BackGrMap, FirstTime);

         if(Clouds != 0 )
         {
               i = Buffers.XView / CloudSpeed;
               PutClouds (i, Buffers.XView - Buffers.LastXView [Game.CurrentPage]);
         }
      }

      public void DrawBackGrMap(int Y1, int Y2, int Shift, byte C)
      {
         int j;
         for(int i = 0; i <= 320 - 1; i++)
         {
               for(j = Y1 - BackGrMap[i + Shift]; j <= Y2; j++)
                 if(Game.GetPixel (i, j) >= 0xC0)
                   Game.PutPixel (i, j, C);
         }
      }

      public void StartClouds()
      {
         if(Clouds == 0)
         {
            for(int i = Buffers.XView + MaxCloudSize; i >= Buffers.XView; i++)
            {
               Buffers.XView = i;
               PutClouds(i / CloudSpeed, -CloudSpeed);
            }  
         }
      }

      public void DrawPalBackGr()
      {
         int i;
         i = Buffers.XView / BrickSpeed;
         if(BackGround == 4)
            BrickPalette (i);
         else if(BackGround == 5)
            LargeBrickPalette (i);
         else if(BackGround == 6)
            PillarPalette (i);
         else if(BackGround == 7)
            WindowPalette (i);
      }

      public void ReadColorMap()
      {
         for (int i = 0; i <= Buffers.NV * Buffers.H - 1; i++)
         {
            ColorMap[i] = Game.GetPixel(Buffers.XView + Shift, i) * 256
               + Game.GetPixel(Buffers.XView + Shift + 1, i);
         }
      }

      public void DrawBricks(int X, int Y, int W, int H)
      {
         Game.PutImage(X, Y, W, H, @PALBRICK000^);
      }

      public void LargeBricks(int X, int Y, int W, int H)
      {
         //asm
         //            push  es
         //            mov   bx, 320
         //            mov   ax, Y
         //  {          add   ax, WindowY }
         //            mul   bx
         //            add   ax, X
         //            mov   di, ax
         //            mov   bl, al
         //            and   bl, 00011111b
         //            add   bl, $E0
         //            mov   ax, VGA_SEGMENT
         //            mov   es, ax
         //            mov   cx, H
         //            mov   dx, Y

         //            push  dx
         //            add   dl, 14          { Why? }
         //            and   dl, 00010000b
         //            or    dl, dl
         //            jz    @0
         //            xor   bl, 16
         //    @0:
         //            pop   dx

         //            jcxz  @End
         //    @1:
         //            push  cx
         //            mov   cx, W
         //            jcxz  @3
         //            push  di

         //            mov   al, $D4
         //            and   dl, 00001111b
         //            cmp   dl, 2
         //            jz    @@1
         //            ja    @Brick
         //            mov   al, $D1
         //            cmp   dl, 0
         //            ja    @@1
         //            mov   al, $D6
         //            xor   bl, 16
         //    @@1:
         //            mov   ah, al
         //            shr   cx, 1
         //            rep   stosw
         //            rcl   cx, 1
         //            rep   stosb
         //            jmp   @LineEnd

         //    @Brick:
         //            mov   al, bl
         //    @2:
         //            and   al, 00011111b
         //            add   al, $E0
         //            stosb
         //            inc   al
         //            dec   cx
         //            jnz   @2

         //    @LineEnd:
         //            pop   di
         //            add   di, 320
         //    @3:
         //            pop   cx
         //            inc   dx
         //            dec   cx
         //            jnz   @1
         //    @End:
         //            pop   es
      }

      public void Pillar(int X, int Y, int W, int H)
      {
         if(((X / W) % 3) == 0)
            Game.PutImage (X, Y, W, H, @PalPill000^);
         else if(((X / W) % 3) == 1)
            Game.PutImage (X, Y, W, H, @PalPill001^);
         else if(((X / W) % 3) == 2)
            Game.PutImage (X, Y, W, H, @PalPill002^);
      }

      public void Windows(int X, int Y, int W, int H)
      {
         const int Y1 =  50;
         const int Y2 =  80;
             //asm
           //          push  es
           //          mov   bx, 320
           //          mov   ax, Y
           //          mov   si, ax
           //          add   si, 22
           //{          add   ax, WindowY }
           //          mul   bx
           //          add   ax, X
           //          mov   di, ax
           //          mov   bl, al
           //          or    bl, $C0
           //          mov   ax, VGA_SEGMENT
           //          mov   es, ax
           //          mov   cx, H
           //          jcxz  @End
           //  @1:
           //          push  cx
           //          mov   cx, W
           //          jcxz  @3

           //          push  di
           //          mov   al, bl

           //          and   si, 00011111b
           //          cmp   si, 00000011b
           //          jb    @4

           //  @2:     and   al, 00011111b
           //          or    al, 11100000b
           //          stosb
           //          inc   al
           //          dec   cx
           //          jnz   @2
           //          jmp   @LineEnd

           //  @4:     mov   ax, 0101h
           //          cld
           //          shr   cx, 1
           //          rep   stosw
           //          rcl   cx, 1
           //          rep   stosb

           //  @LineEnd:
           //          pop   di
           //          add   di, 320
           //  @3:
           //          pop   cx
           //          inc   dx
           //          inc   si
           //          dec   cx
           //          jnz   @1
           //  @End:
           //          pop   es
      }

      public void DrawBackGrBlock(int X, int Y, int W, int H)
      {
         if (Buffers.Options.SkyType == 2 ||
             Buffers.Options.SkyType == 5 ||
             Buffers.Options.SkyType == 9 ||
             Buffers.Options.SkyType == 10 ||
             Buffers.Options.SkyType == 11)
            SmoothFill (X, Y, W, H);
         else if(BackGround == 4)
            DrawBricks (X, Y, W, H);
         else if(BackGround == 5)
            LargeBricks (X, Y, W, H);
         else if(BackGround == 6)
            Pillar (X, Y, W, H);
         else if(BackGround == 7)
            Windows (X, Y, W, H);
         else
            for(int i = 0; i <= H - 1; i++)
               Game.Fill (X, Y + i, W, 1, ColorMap [Y + i]);
      }

      public void SmoothFill(int X, int Y, int W, int H)
      {
         ushort PageOffset = Game.GetPageOffset();
         int Horizon = Buffers.Options.Horizon - 4;
         //asm
         //        push    es
         //        mov     ax, VGA_SEGMENT
         //        mov     es, ax

         //        mov     dx, Y
         //        mov     ax, VIR_SCREEN_WIDTH / 4
         //        mul     dx
         //        mov     di, X
         //        shr     di, 1
         //        shr     di, 1
         //        add     di, ax
         //        add     di, PageOffset

         //        mov     ax, Y
         //        cmp     ax, Horizon
         //        jb      @0
         //        mov     dl, $F0
         //        jmp     @3

         //    @0: mov     bl, 6
         //        div     bl
         //        mov     dl, $EF
         //        sub     dl, al
         //        cmp     dl, $E0
         //        jnb     @3
         //        mov     dl, $E0
         //    @3: mov     dh, ah

         //        mov     bx, H
         //        cmp     bx, 0
         //        jle     @End
         //        mov     cx, W
         //        shr     cx, 1
         //        shr     cx, 1

         //        cld

         //    @1: push    di
         //        push    cx
         //        push    dx
         //        mov     ah, 0Fh
         //        mov     al, MAP_MASK
         //        mov     dx, SC_INDEX
         //        out     dx, ax
         //        pop     dx
         //        mov     al, dl
         //        mov     ah, al
         //        shr     cx, 1
         //        rep     stosw
         //        rcl     cx, 1
         //        rep     stosb
         //        pop     cx
         //        pop     di

         //        cmp     dh, 3
         //        jb      @4
         //        cmp     al, $E0
         //        jz      @2
         //        cmp     al, $F0
         //        jz      @2
         //        sub     ax, 0101h
         //    @2: push    ax
         //        push    dx
         //        mov     ah, 0101b
         //        push    cx
         //        mov     cl, dh
         //        and     cl, 1
         //        shl     ah, cl
         //        pop     cx
         //        mov     al, MAP_MASK
         //        mov     dx, SC_INDEX
         //        out     dx, ax
         //        pop     dx
         //        pop     ax
         //        push    di
         //        push    cx
         //        shr     cx, 1
         //        rep     stosw
         //        rcl     cx, 1
         //        rep     stosb
         //        pop     cx
         //        pop     di

         //    @4: inc     Y
         //        mov     ax, Y
         //        cmp     ax, Horizon
         //        jb      @9
         //        mov     dl, $F0
         //    @9: inc     dh
         //        cmp     dh, 6
         //        jnz     @5
         //        mov     dh, 0
         //        cmp     dl, $E0
         //        jz      @5
         //        cmp     dl, $F0
         //        jz      @5
         //        dec     dl
         //    @5: add     di, VIR_SCREEN_WIDTH / 4
         //        dec     bx
         //        jnz     @1

         //  @End:
         //        pop     es
      }

      private void InitClouds()
      {
         int i, j, Tmp0, Tmp1;
         CloudMap [1, 0] =   50; CloudMap [1, 1] =  58; CloudMap [MaxClouds + 1, 0] =   92;
         CloudMap [2, 0] =  180; CloudMap [2, 1] =  20; CloudMap [MaxClouds + 2, 0] =  228;
         CloudMap [3, 0] =  430; CloudMap [3, 1] =  40; CloudMap [MaxClouds + 3, 0] =  484;
         CloudMap [4, 0] =  570; CloudMap [4, 1] =  15; CloudMap [MaxClouds + 4, 0] =  600;
         CloudMap [5, 0] =  840; CloudMap [5, 1] =  30; CloudMap [MaxClouds + 5, 0] =  900;
         CloudMap [6, 0] =  980; CloudMap [6, 1] =  60; CloudMap [MaxClouds + 6, 0] = 1040;
         CloudMap [7, 0] = 1200; CloudMap [7, 1] =  20; CloudMap [MaxClouds + 7, 0] = 1240;
      }

      private void TraceCloud(int X, int Y, int N, byte Dir, byte Attr, byte Ovr)
      {
         int min, max;
         byte ok;
           //  asm
           //        jmp     @Start

           //  @PutList:     { SI = Offset, AH = Count }
           //        mov     Ok, 0
           //        push    ax
           //        segcs   lodsw
           //        add     di, ax
           //        push    cx
           //        push    di
           //  @@0:  seges   mov     al, [di]
           //        cmp     al, bl
           //        jnz     @@1
           //        cmp     di, Min
           //        jb      @@2
           //        cmp     di, Max
           //        ja      @@2
           //        seges   mov     [di], dl
           //        mov     Ok, 1
           //        jmp     @@1
           //  @@2:  cmp     Ok, 1
           //        jnz     @@1
           //        jmp     @@3
           //  @@1:  inc     di
           //        dec     cx
           //        jnz     @@0
           //  @@3:  pop     di
           //        add     di, 320
           //        pop     cx
           //        pop     ax
           //        add     Min, 320
           //        add     Max, 320
           //        dec     ah
           //        jnz     @PutList
           //        retn

           //  @Start:
           //        push    es
           //        mov     ax, VGA_SEGMENT
           //        mov     es, ax
           //        cld
           //        mov     bx, 320
           //        mov     ax, Y
           //{        add     ax, WindowY }
           //        mul     bx
           //        push    ax
           //        add     ax, XView
           //        mov     Min, ax
           //        mov     Max, ax
           //        pop     ax
           //        add     Max, 320 - 1
           //        add     ax, X
           //        mov     di, ax
           //        mov     dl, Attr

           //        cmp     Dir, Right
           //        jz      @Right
           //  @Left:
           //        call    @GetLeftList

           //        dw      9, -3, -2, -1, -1, -1, 0, -1, 0, 0, 0, 0, 1
           //        dw      0, 1, 1, 1, 2, 3

           //  @GetLeftList:
           //        pop     si
           //        mov     ah, 19
           //        mov     bl, Ovr
           //        mov     cx, N
           //        jcxz    @End
           //        call    @PutList
           //        jmp     @End

           //  @Right:
           //        call    @GetRightList

           //        dw      0, 3, 2, 1, 1, 1, 0, 1, 0, 0, 0, 0, -1, 0, -1
           //        dw      -1, -1, -2, -3

           //  @GetRightList:
           //        pop     si
           //        mov     ah, 19
           //        mov     bl, Ovr
           //        mov     cx, N
           //        jcxz    @End
           //        call    @PutList
           //  @End:
           //        pop     es
      }

      private void PutClouds(int Offset, int N)
      {
         int i, X1, X2, Y;
         byte Attr, Ovr, Size, XSize;
         if (Clouds != 0)
         {
            i = 1;
            while (i <= MaxClouds)
            {
               Attr = Clouds;
               Ovr = 0xE0;
               X1 = Buffers.XView - Offset + CloudMap[i, 0];
               X2 = Buffers.XView - Offset + CloudMap[i + MaxClouds, 0];
               XSize = Convert.ToByte(X2 - X1 + 1);
               Y = CloudMap[i, 1];

               if (N > 0)
               {
                  Size = 0;
                  if ((X2 + 10) >= (Buffers.XView + Buffers.NH * Buffers.W))
                     Size = 10;
                  if (((X2 + 10) > Buffers.XView) && (X2 < (Buffers.XView + Buffers.NH * Buffers.W + 10)))
                     TraceCloud(X2 - N - Size, Y, N + Size, Right, Attr, Ovr);
                  if (((X1 + 10) > Buffers.XView) && (X1 < (Buffers.XView + Buffers.NH * Buffers.W)))
                  {
                     TraceCloud(X1 - N, Y, N, Left, Ovr, Attr);
                     if (X2 >= (Buffers.XView + Buffers.NH * Buffers.W))
                        TraceCloud(X1, Y, XSize, Left, Attr, Ovr);
                  }
               }
               if (N < 0)
               {
                  if (((X2 + 10) > Buffers.XView) && (X2 < (Buffers.XView + Buffers.NH * Buffers.W + 10)))
                  {
                     TraceCloud (X2, Y, - N, Right, Ovr, Attr);
                     if (X1 <= (Buffers.XView - 10))
                        TraceCloud (X2 - XSize, Y, XSize, Right, Attr, Ovr);
                  }
                  Size = 0;
                  if (X1 < (Buffers.XView + 10))
                     Size = 10;
                  if (((X1 + 10) > Buffers.XView) && (X1 < (Buffers.XView + Buffers.NH * Buffers.W + 10)))
                     TraceCloud (X1, Y, - N + Size, Left, Attr, Ovr);
               }
               i++;
            }
         }
      }

      private void PutBackGr(Array Map, bool Fill)
      {
         int Y, PageOffset, X1, X2, XPos, X1Pos, X2Pos, DX, OldXView, XStart, OldXStart, Count;
         byte Bank;

         PageOffset = Game.GetPageOffset();
         OldXView = Buffers.LastXView[Game.CurrentPage()];
         Y = PageOffset + (Buffers.Options.Horizon - HEIGHT) * Game.BYTES_PER_LINE;
         X1 = Y + Buffers.XView / 4;
         X2 = Y + (Buffers.XView + Buffers.NH * Buffers.W) / 4;
         Bank = Buffers.XView & 3;
         DX = Buffers.XView - OldXView;
         XPos = Buffers.XView;
         X1Pos = Buffers.XView;
         X2Pos = OldXView + Buffers.NH * Buffers.W - 1;
         if (DX < 0)
         {
            X1Pos = OldXView;
            X2Pos = Buffers.XView + Buffers.NH * Buffers.W - 1;
         }
         XStart = Buffers.XView / Speed;
         OldXStart = OldXView / Speed + DX;
          //     asm
          //      push    ds
          //      push    es
          //      mov     ax, VGA_SEGMENT
          //      mov     es, ax
          //      lds     si, Map
          //      cld
          //      mov     Count, 4
          //@1:   mov     cl, Bank
          //      mov     ah, 1
          //      shl     ah, cl
          //      mov     al, MAP_MASK
          //      mov     dx, SC_INDEX
          //      out     dx, ax
          //      mov     ah, cl
          //      mov     al, READ_MAP
          //      mov     dx, GC_INDEX
          //      out     dx, ax
          //      mov     dx, XPos
          //      mov     al, $F0
          //      mov     di, X1
          //      mov     cx, OldXStart
          //      mov     bx, XStart
          //@4:   push    bx
          //      push    cx
          //      push    dx
          //      push    di
          //      mov     ah, [bx + si]  { new position }
          //      mov     bx, cx
          //      mov     cl, [bx + si]  { old position }
          //      mov     ch, 0
          //      cmp     Fill, 0
          //      jnz     @Fill
          //      cmp     dx, X1Pos
          //      jb      @Fill
          //      cmp     dx, X2Pos
          //      ja      @Fill
          //      cmp     ah, cl
          //      jz      @5
          //      jl      @8
          //@6:   push    ax
          //      mov     ax, BYTES_PER_LINE
          //      mul     cx
          //      add     di, ax
          //      pop     ax
          //@7:   seges   cmp     [di], al
          //      jnz     @@2
          //      sub     al, $10
          //      seges   mov     [di], al
          //      add     al, $10
          //@@2:  add     di, BYTES_PER_LINE
          //      inc     cl
          //      cmp     cl, ah
          //      jb      @7
          //      jmp     @5
          //@8:   push    ax
          //      mov     bx, BYTES_PER_LINE
          //      mov     al, ah
          //      mov     ah, 0
          //      mul     bx
          //      add     di, ax
          //      pop     ax
          //@9:   sub     al, $10
          //      seges   cmp     [di], al
          //      pushf
          //      add     al, $10
          //      popf
          //      jnz     @@1
          //      seges   mov     [di], al
          //@@1:  add     di, BYTES_PER_LINE
          //      inc     ah
          //      cmp     ah, cl
          //      jb      @9
          //@5:   pop     di
          //      pop     dx
          //      pop     cx
          //      pop     bx
          //      add     bx, 4
          //      add     cx, 4
          //      add     dx, 4
          //      inc     di
          //      cmp     di, X2
          //      jb      @4
          //@2:   inc     Bank
          //      cmp     Bank, 4
          //      jnz     @3
          //      and     Bank, 3
          //      inc     X1
          //      inc     X2
          //@3:   inc     OldXStart
          //      inc     XStart
          //      inc     XPos
          //      dec     Count
          //      jnz     @1
          //      pop     es
          //      pop     ds
          //      jmp     @Exit

          //@Fill:
          //      push    bx
          //      push    cx
          //      mov     cl, ch
          //      mov     ch, 0
          //      mov     bl, ah
          //      mov     bh, 0
          //@@5:  cmp     cx, HEIGHT
          //      ja      @@3
          //      cmp     cx, bx
          //      jb      @@4
          //      sub     al, $10
          //      seges   cmp     [di], al
          //      pushf
          //      add     al, $10
          //      popf
          //      jnz     @@7
          //      seges   mov     [di], al
          //@@7:  add     di, BYTES_PER_LINE
          //      inc     cx
          //      jmp     @@5
          //@@4:  seges   cmp     [di], al
          //      jnz     @@6
          //      sub     al, $10
          //      seges   mov     [di], al
          //      add     al, $10
          //@@6:  add     di, BYTES_PER_LINE
          //      inc     cx
          //      jmp     @@5
          //@@3:  pop     cx
          //      pop     bx
          //      jmp     @5

          //@Exit:
      }

      private void BrickPalette(int i)
      {
         i = i % 20;
         for (int j = 0; j <= 19; j++)
         {
            if (i == j)
               Palettes.CopyPalette(0xFE, 0xE0 + j);
            else if (((i + 2) % 20) == j)
               Palettes.CopyPalette(0xFF, 0xE0 + j);
            else
               Palettes.CopyPalette(0xFD, 0xE0 + j);
         }
      }

      private void LargeBrickPalette(int i)
      {
         i = i % 32;
         for (int j = 0; j <= 31; j++)
         {
            if ((i == j) || (((i + 1) % 32) == j))
               Palettes.CopyPalette (0xD6, 0xE0 + j);
            else if ((((i + 3) % 32) == j) || (((i + 4) % 32) == j))
               Palettes.CopyPalette (0xD4, 0xE0 + j);
            else
               Palettes.CopyPalette (0xD1, 0xE0 + j);
         }
      }

      private void PillarPalette(int i)
      {
         const int ShadowPos = 28;
         const int ShadowEnd = 36;
         int j, k, l;
         byte c1, c2, c3, Base;
         Base = Buffers.Options.BackGrColor1;
         c1 = Palette [Base, 0] / 4; //Palette is a Unit used for manipulating any color in 256 color video mode
         c2 = Palette [Base, 1] / 4; //Going to need to use something to replace palette.
         c3 = Palette [Base, 2] / 4;
         i = i % 60;
         j = 0;
         k = 1;
         while( k < 15)
         {
           for (l = j; l <= k; l++)
           {
              Palettes.OutPalette (0xC0 + ((l + i) % 60), c1 + k, c2 + k, c3 + k);
              Palettes.OutPalette (0xC0 + ((ShadowPos + i - l) % 60), c1 + k, c2 + k, c3 + k);
           }
           j = k;
           k = k + 1;
         }
         for(j = ShadowPos; j <= ShadowEnd; j++)
         {
            if (c1 > 0)
                c1--;
            if (c2 > 0)
                c2--;
            if (c3 > 0)
                c3--;
            Palettes.OutPalette (0xC0 + ((j + i) % 60), c1, c2, c3);
         }
         Base = Buffers.Options.BackGrColor2;
         c1 = Palette [Base, 0] / 4;
         c2 = Palette [Base, 1] / 4;
         c3 = Palette [Base, 2] / 4;
         for( j = ShadowEnd + 1; j <= 59; j++)
              Palettes.OutPalette (0xC0 + ((i + j) % 60), c1, c2, c3);
      }

      private void WindowPalette(int i)
      {
       int j;
       i = i % 32;
       for (j = 0; j <= 5; j++)
          Palettes.CopyPalette (1, 0xE0 + ((i + j) % 32));
       for (j = 6; j <= 31; j++)
          Palettes.CopyPalette (16, 0xE0 + ((i + j) % 32));
      }
   }
}